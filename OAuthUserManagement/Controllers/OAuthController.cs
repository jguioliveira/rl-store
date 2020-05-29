using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.OAuth.Controllers
{
    public class OAuthController : Controller
    {
        public IActionResult Authenticate()
        {
            return View();
        }
    }
}
