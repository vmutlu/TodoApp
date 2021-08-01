using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Utilities.Results;
using ToDo.Entities.Concrate;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery) => Ok(await _categoryService.GetAllAsync(paginationQuery));

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _categoryService.GetByIdAsync(id));

        [HttpPost("add")]
        public async Task<IActionResult> Add(Category category) => Ok(await _categoryService.AddAsync(category));

        [HttpPut("update")]
        public async Task<IActionResult> Update(Category category) => Ok(await _categoryService.UpdateAsync(category));

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _categoryService.DeleteAsync(id));
    }
}
