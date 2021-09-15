using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Business.Constants;
using ToDo.Business.ValidationRules.FluentValidation;
using ToDo.Core.Aspects.Autofac.Caching;
using ToDo.Core.Aspects.Autofac.Validation;
using ToDo.Core.Entities;
using ToDo.Core.Extensions;
using ToDo.Core.Services.Abstract;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Concrete
{
    public class TodoManager : ITodoService
    {
        private readonly ITodoDal _todoDal;
        private readonly IPaginationUriService _uriService;
        public TodoManager(ITodoDal todoDal, IPaginationUriService uriService) { _todoDal = todoDal; _uriService = uriService; }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITodoService.Get")]
        [ValidationAspect(typeof(TodoValidator))]
        public async Task<IResult> AddAsync(Todo tEntity)
        {
            await _todoDal.AddAsync(tEntity);

            return new Result(success: true, message: "TODO Başarıyla Eklenmiştir.");
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITodoService.Get")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleted = await _todoDal.GetAsync(i => i.Id == id);
            await _todoDal.DeleteAsync(deleted);

            return new Result(success: true, message: "TODO Başarıyla Silinmiştir."); ;
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<PagingResult<Todo>> GetAllAsync(GeneralFilter generalFilter = null)
        {
            var query = await _todoDal.GetAllForPagingAsync(generalFilter.Page, generalFilter.PropertyName, generalFilter.Asc, null, c => c.Category).ConfigureAwait(false);

            var response = (from todo in query.Data
                            select new Todo()
                            {
                                CategoryId = todo.CategoryId,
                                Content = todo.Content,
                                DueDate = todo.DueDate,
                                Id = todo.Id,
                                IsFavorite = todo.IsFavorite,
                                ReminMeDate = todo.ReminMeDate,
                                Category = todo.Category != null ? new Category()
                                {
                                    CategoryId = todo.Category.CategoryId,
                                    Name = todo.Category.Name
                                } : null
                            }).ToList();

            return new PagingResult<Todo>(response, query.TotalItemCount, query.Success, query.Message);
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<Todo>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<Todo>(null, Messages.InvalidTodoId);

            var todoObject = await _todoDal.GetAsync(c => c.Id == id, t => t.Category).ConfigureAwait(false);

            if (todoObject is null)
                return new ErrorDataResult<Todo>(null, Messages.InvalidTodo);

            Todo todo = new Todo()
            {
                CategoryId = todoObject.CategoryId,
                Content = todoObject.Content,
                DueDate = todoObject.DueDate,
                Id = todoObject.Id,
                IsFavorite = todoObject.IsFavorite,
                ReminMeDate = todoObject.ReminMeDate,
                Category = todoObject.Category != null ? new Category()
                {
                    CategoryId = todoObject.Category.CategoryId,
                    Name = todoObject.Category.Name
                } : null
            };

            return new SuccessDataResult<Todo>(todo);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITodoService.Get")]
        public async Task<IResult> UpdateAsync(Todo todo)
        {
            var updatedTodo = await _todoDal.GetAsync(i => i.Id == todo.Id);

            updatedTodo.Content = todo.Content;

            if (todo.CategoryId != 0)
                updatedTodo.CategoryId = todo.CategoryId;

            updatedTodo.DueDate = todo.DueDate;
            updatedTodo.IsFavorite = todo.IsFavorite;

            await _todoDal.UpdateAsync(updatedTodo);

            return new Result(success: true, message: "TODO Başarıyla Güncellenmiştir.");
        }
    }
}
