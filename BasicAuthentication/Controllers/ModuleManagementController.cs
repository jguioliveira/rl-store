using BasicAuthentication.Domain.Entities;
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

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(NewModule newModule)
        {
            var module = new Module(newModule.Name, newModule.Active);
            await _moduleRepository.CreateAsync(module);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var module = await _moduleRepository.GetAsync(id);

            var editModule = new NewModule
            {
                Name = module.Name,
                Active = module.Active
            };

            return View(editModule);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string id, NewModule newModule)
        {
            var module = new Module(newModule.Name, newModule.Active);
            await _moduleRepository.UpdateAsync(id, module);

            return RedirectToAction("Index");
        }
    }
}