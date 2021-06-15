using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _todoService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _todoService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Todo todo)
        {
            var response = await _todoService.AddAsync(todo);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Todo todo)
        {
            var response = await _todoService.UpdateAsync(todo);
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _todoService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
