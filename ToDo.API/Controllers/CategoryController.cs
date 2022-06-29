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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IPaginationResult<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAll(GeneralFilter generalFilter)
        {
            var result = await _categoryService.GetAllAsync(generalFilter);
            return this.GetResult(result);
        }

        [HttpGet("getById/{id}")]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Add(Category entity)
        {
            var result = await _categoryService.AddAsync(entity);
            return this.GetResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Update(Category entity)
        {
            var result = await _categoryService.UpdateAsync(entity);
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
            var result = await _categoryService.DeleteAsync(id);
            return this.GetResult(result);
        }
    }
}
