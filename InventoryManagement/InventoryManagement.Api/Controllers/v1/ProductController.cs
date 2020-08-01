using System.Threading.Tasks;
using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IHandler<CreateProductCommand> _createHandler;
        private readonly IHandler<UpdateProductCommand> _updateHandler;
        private readonly IHandler<UpdateInventoryCommand> _updateInventoryHandler;
        private readonly IHandler<UpdateBookMarksCommand> _updateBookMarksHandler;

        public ProductController(
            IHandler<CreateProductCommand> createHandler,
            IHandler<UpdateProductCommand> updateHandler,
            IHandler<UpdateInventoryCommand> updateInventoryHandler,
            IHandler<UpdateBookMarksCommand> updateBookMarksHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _updateInventoryHandler = updateInventoryHandler;
            _updateBookMarksHandler = updateBookMarksHandler;
        }

        [HttpGet]        
        public IActionResult Get()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductCommand createCommand)
        {
            var result = await _createHandler.HandleAsync(createCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductCommand updateCommand)
        {
            var result = await _updateHandler.HandleAsync(updateCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}/inventory")]
        public async Task<IActionResult> UpdateInventoryAsync(string id, UpdateInventoryCommand updateCommand)
        {
            if (string.IsNullOrEmpty(updateCommand.ProductId))
                updateCommand.ProductId = id;

            var result = await _updateInventoryHandler.HandleAsync(updateCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}/productbookmark")]
        public async Task<IActionResult> UpdateProductBookMarkAsync(string id, UpdateBookMarksCommand updateCommand)
        {
            if (string.IsNullOrEmpty(updateCommand.ProductId))
                updateCommand.ProductId = id;

            var result = await _updateBookMarksHandler.HandleAsync(updateCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
