using System.Linq;
using System.Threading.Tasks;
using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel.UserManagement;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAsync();
            var indexModel = new Index
            {
                Users = users.Select(u => new UserData
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Active = u.Active
                })
            };

            return View(indexModel);
        }

        public async Task<IActionResult> Edit(string id)
        {            

            return View();
        }
    }
}