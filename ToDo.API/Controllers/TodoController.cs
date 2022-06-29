using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ToDo.API.Extensions;
using ToDo.Business.Abstract;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService) => (_todoService) = (todoService);


        [HttpGet]
        [ProducesResponseType(typeof(IPaginationResult<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll(GeneralFilter generalFilter)
        {
            var result = await _todoService.GetAllAsync(generalFilter);
            return this.GetResult(result);
        }

        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _todoService.GetByIdAsync(id);
            return this.GetResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Add(Todo entity)
        {
            var result = await _todoService.AddAsync(entity);
            return this.GetResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(Todo entity)
        {
            var result = await _todoService.UpdateAsync(entity);
            return this.GetResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _todoService.DeleteAsync(id);
            return this.GetResult(result);
        }
    }
}
