using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class ModuleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}