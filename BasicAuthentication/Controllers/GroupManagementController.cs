using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.ValueObjects;
using BasicAuthentication.ViewModel.GroupManagement;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class GroupManagementController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IModuleRepository _moduleRepository;

        public GroupManagementController(
            IGroupRepository groupRepository,
            IModuleRepository moduleRepository)
        {
            _groupRepository = groupRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAsync();

            var indexModel = new Index()
            {
                Groups = groups.Select(g => new GroupData
                {
                    Id = g.Id,
                    Name = g.Name
                })
            };

            return View(indexModel);
        }

        public async Task<IActionResult> New()
        {
            var modules = await _moduleRepository.GetAsync();

            NewGroup newGroup = new NewGroup
            {
                PermissionModules = modules.Select(m => new PermissionModuleData
                {
                    ModuleId = m.Id,
                    ModuleName = m.Name,
                    Insert = false,
                    Update = false,
                    Delete = false,
                    Select = false
                }).ToList()
            };

            return View(newGroup);
        }

        [HttpPost]
        public async Task<IActionResult> New(NewGroup newGroup)
        {
            var group = new Group(newGroup.Name);

            group.AddPermissionModuleRange(
                newGroup
                .PermissionModules
                .Select(p => new PermissionModule(
                    p.ModuleId,
                    new Permission(p.Select, p.Insert, p.Update, p.Delete)
                    )
                ));

            await _groupRepository.CreateAsync(group);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string id)
        {
            var groupTask = _groupRepository.GetAsync(id);
            var modulesTask = _moduleRepository.GetAsync();

            await Task.WhenAll(groupTask, modulesTask);

            var group = groupTask.Result;
            var modules = modulesTask.Result;

            NewGroup editGroup = new NewGroup
            {
                Name = group.Name,
                PermissionModules = group.PermissionModules.Select(p => new PermissionModuleData
                {
                    ModuleId = p.ModuleId,
                    ModuleName = modules.SingleOrDefault(m => m.Id == p.ModuleId)?.Name,
                    Insert = p.Permission.Insert,
                    Update = p.Permission.Update,
                    Delete = p.Permission.Delete,
                    Select = p.Permission.Select
                }).ToList()
            };

            return View(editGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, NewGroup editedGroup)
        {
            var group = new Group(editedGroup.Name);

            group.AddPermissionModuleRange(
                editedGroup
                .PermissionModules
                .Select(p =>new PermissionModule (
                    p.ModuleId, 
                    new Permission(p.Select, p.Insert, p.Update, p.Delete)
                    )
                ));

            await _groupRepository.UpdateAsync(id, group);

            return RedirectToAction("Index");
        }
    }
}