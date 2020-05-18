using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasicAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            else
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignIn userData)
        {
            var user = await _userRepository.GetAsync(userData.Email);

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

                    RedirectUri = "/Home/SignIn"
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    "BasicAuth",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Account");
            }

            return RedirectToAction("Index");
        }
    }
}