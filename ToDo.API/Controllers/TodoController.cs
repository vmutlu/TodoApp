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
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService) => _todoService = todoService;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() => Ok(await _todoService.GetAllAsync());

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _todoService.GetByIdAsync(id));

        [HttpPost("add")]
        public async Task<IActionResult> Add(Todo todo) => Ok(await _todoService.AddAsync(todo));

        [HttpPut("update")]
        public async Task<IActionResult> Update(Todo todo) => Ok(await _todoService.UpdateAsync(todo));

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _todoService.DeleteAsync(id));
    }
}
