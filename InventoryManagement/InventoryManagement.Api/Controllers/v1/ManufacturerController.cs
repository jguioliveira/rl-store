using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Handlers;

namespace InventoryManagement.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IHandler<CreateManufacturerCommand> _createHandler;
        private readonly IHandler<UpdateManufacturerCommand> _updateHandler;

        public ManufacturerController(
            IHandler<CreateManufacturerCommand> createHandler,
            IHandler<UpdateManufacturerCommand> updateHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateManufacturerCommand createCommand)
        {
            var result = await _createHandler.HandleAsync(createCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateManufacturerCommand updateCommand)
        {
            var result = await _updateHandler.HandleAsync(updateCommand);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }
    }
}
