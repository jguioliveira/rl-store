using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel.ModuleManagement;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.Controllers
{
    public class ModuleManagementController : Controller
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleManagementController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var modules = await _moduleRepository.GetAsync();

            Index indexModel = new Index
            {
                Modules = modules.Select(m => new ModuleData
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Active = m.Active
                })
            };

            return View(indexModel);
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}