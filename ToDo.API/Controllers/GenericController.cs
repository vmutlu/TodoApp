using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using ToDo.API.Extensions;
using ToDo.Business.Abstract;
using ToDo.Core.Entities;
using ToDo.Core.Utilities.Results;

namespace Api.Controllers
{
    [
        ApiController,
        Produces(MediaTypeNames.Application.Json),
        Route("api/[controller]/[action]")
    ]
    public abstract class GenericController<T, TKey, TService> : ControllerBase where T : class, IEntity, new() where TKey : IEquatable<TKey> where TService : IServiceBase<T, TKey>
    {
        private readonly TService service;
        protected GenericController(TService service) =>
            this.service = service;


        [HttpGet]
        [ProducesResponseType(typeof(IPaginationResult<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> GetAll([FromQuery] GeneralFilter generalFilter)
        {
            var result = await service?.GetAllAsync(generalFilter);
            return this.GetResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IDataResult<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> GetById(TKey id)
        {
            var result = await service?.GetByIdAsync(id);
            return this.GetResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Add(T entity)
        {
            var result = await service?.AddAsync(entity);
            return this.GetResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Update(T entity)
        {
            var result = await service?.UpdateAsync(entity);
            return this.GetResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            var result = await service?.DeleteAsync(id);
            return this.GetResult(result);
        }
    }
}