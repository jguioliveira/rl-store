using System.Linq;
using System.Threading.Tasks;
using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.ViewModel.GroupManagement;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers
{
    public class GroupManagementController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupManagementController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupRepository.GetAsync();

            var indexModel = new Index()
            {
                Groups = groups.Select(g => new GroupData
                {
                    Id = g.Id.ToString(),
                    Name = g.Name
                })
            };

            return View(indexModel);
        }
    }
}