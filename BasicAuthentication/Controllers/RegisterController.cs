using BasicAuthentication.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicAuthentication.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewUser newUser)
        {
            return RedirectToAction("EmailConfirmation");
        }

        public IActionResult EmailConfirmation()
        {
            return View();
        }
    }    
}