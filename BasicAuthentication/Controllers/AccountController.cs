using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BasicAuthentication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserDomain.Entities;
using UserDomain.Repositories;

namespace BasicAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController(
            UserManager<User> userManager,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserSignIn userData)
        {
            var user = await _userRepository.GetByEmail(userData.Email);

            if (!(user is null) && user.CheckPassword(userData.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, "BasicAuth");

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = userData.RememberMe,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTimeOffset.UtcNow,
                    // The time at which the authentication ticket was issued.

                    RedirectUri = "/Account/Login"
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    "BasicAuth",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewUser newUser)
        {
            var user = new User(newUser.Email, newUser.FirstName, newUser.LastName, newUser.Password);

            await _userRepository.CreateUserAsync(user);

            return RedirectToAction("SendEmailConfirmation");
        }

        public IActionResult SendEmailConfirmation()
        {
            return View();
        }
    }
}