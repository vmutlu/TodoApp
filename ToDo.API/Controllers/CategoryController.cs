using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Category category)
        {
            var response = await _categoryService.AddAsync(category);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Category category)
        {
            var response = await _categoryService.UpdateAsync(category);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
