using Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Business.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController :  GenericController<Todo, int, ITodoService>
    {
        public TodoController(ITodoService todoService) : base(todoService) { }
    }
}
