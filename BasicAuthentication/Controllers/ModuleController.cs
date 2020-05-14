using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel.ModuleManagement;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleController(IModuleRepository moduleRepository)
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