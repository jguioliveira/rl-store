using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class GroupManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}