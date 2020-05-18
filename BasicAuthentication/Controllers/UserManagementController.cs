using System.Linq;
using System.Threading.Tasks;
using BasicAuthentication.Domain.Entities;
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

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(NewUser newUser)
        {
            var user = new User(newUser.Email, newUser.FirstName, newUser.LastName, newUser.Password, newUser.Active);

            await _userRepository.CreateUserAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            NewUser newUser = new NewUser
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Active = user.Active,
            };

            return View(newUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, NewUser newUser)
        {
            var user = new User(newUser.Email, newUser.FirstName, newUser.LastName, newUser.Password, newUser.Active);

            await _userRepository.UpdateAsync(id, user);

            return RedirectToAction("Index");
        }
    }
}