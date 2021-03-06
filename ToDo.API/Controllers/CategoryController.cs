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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            var response = await _categoryService.AddAsync(category);
            return Ok(response);
        }
    }
}
