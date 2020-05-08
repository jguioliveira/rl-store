using BasicAuthentication.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserDomain.Entities;
using UserDomain.Repositories;

namespace BasicAuthentication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(NewUser newUser)
        {
            var user = new User(newUser.Email, newUser.FirstName, newUser.LastName, newUser.Password);

            await _userRepository.CreateUserAsync(user);

            return RedirectToAction("EmailConfirmation");
        }

        public IActionResult EmailConfirmation()
        {
            return View();
        }
    }    
}