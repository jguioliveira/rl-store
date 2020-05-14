using System.Threading.Tasks;
using BasicAuthentication.Domain.Entities;
using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("BasicAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}