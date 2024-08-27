﻿using System.Threading.Tasks;
using Asp.Versioning;
using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class CategoryController : ControllerBase
    {
        private readonly IHandler<CreateCategoryCommand> _createHandler;
        private readonly IHandler<UpdateCategoryCommand> _updateHandler;

        public CategoryController(
            IHandler<CreateCategoryCommand> createHandler,
            IHandler<UpdateCategoryCommand> updateHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryCommand createCommand)
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
        public async Task<IActionResult> UpdateAsync(UpdateCategoryCommand updateCommand)
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
