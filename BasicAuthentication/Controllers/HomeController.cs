using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }    
}