using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Core.Aspects.Autofac.Caching;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Concrete
{
    public class TodoManager : ITodoService
    {
        private readonly ITodoDal _todoDal;
        public TodoManager(ITodoDal todoDal)
        {
            _todoDal = todoDal;
        }

        [SecuredOperation("admin")]
        public async Task<IResult> AddAsync(Todo tEntity)
        {
            await _todoDal.AddAsync(tEntity);

            return new Result(success: true, message: "TODO Başarıyla Eklenmiştir.");
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleted = await _todoDal.GetAsync(i=>i.Id == id);
            await _todoDal.DeleteAsync(deleted);

            return new Result(success: true, message: "TODO Başarıyla Eklenmiştir."); ;
        }

        [CacheAspect]
        public async Task<IDataResult<List<Todo>>> GetAllAsync()
        {
            return new SuccessDataResult<List<Todo>>(await _todoDal.GetAllAsync());
        }

        [CacheAspect]
        public async Task<IDataResult<Todo>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<Todo>(await _todoDal.GetAsync(c => c.Id == id));
        }

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(Todo todo)
        {
            var updatedTodo = await _todoDal.GetAsync(i=>i.Id == todo.Id);

            updatedTodo.Content = todo.Content;
            updatedTodo.CategoryId = todo.CategoryId;
            updatedTodo.DueDate = todo.DueDate;
            updatedTodo.IsFavorite = todo.IsFavorite;

            await _todoDal.UpdateAsync(updatedTodo);

            return new Result(success: true, message: "TODO Başarıyla Güncellenmiştir.");
        }
    }
}
