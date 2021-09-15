using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Business.Constants;
using ToDo.Business.ValidationRules.FluentValidation;
using ToDo.Core.Aspects.Autofac.Caching;
using ToDo.Core.Aspects.Autofac.Transaction;
using ToDo.Core.Aspects.Autofac.Validation;
using ToDo.Core.Entities;
using ToDo.Core.Extensions;
using ToDo.Core.Extensions.MapHelper;
using ToDo.Core.Services.Abstract;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IPaginationUriService _uriService;
        public CategoryManager(ICategoryDal categoryDal, IPaginationUriService uriService) { _categoryDal = categoryDal; _uriService = uriService; }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> AddAsync(Category category)
        {
            await _categoryDal.AddAsync(category);

            return new Result(success: true, message: "Kategori Başarıyla Eklenmiştir.");
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleted = await _categoryDal.GetAsync(i => i.CategoryId == id);
            await _categoryDal.DeleteAsync(deleted);

            return new Result(success: true, message: "Kategori Başarıyla Silinmiştir.");
        }

        //  [SecuredOperation("admin,user")]
        // [CacheAspect]
        public async Task<PagingResult<Category>> GetAllAsync(GeneralFilter generalFilter = null)
        {
            var query = await _categoryDal.GetAllForPagingAsync(generalFilter.Page, generalFilter.PropertyName, generalFilter.Asc, null, c => c.Todos).ConfigureAwait(false);
            var response = (from category in query.Data
                            select new Category()
                            {
                                CategoryId = category.CategoryId,
                                Name = category.Name,
                                Todos = category.Todos != null ? (from ct in category.Todos
                                                                  select new Todo()
                                                                  {
                                                                      CategoryId = ct.CategoryId,
                                                                      Content = ct.Content,
                                                                      DueDate = ct.DueDate,
                                                                      Id = ct.Id,
                                                                      IsFavorite = ct.IsFavorite,
                                                                      ReminMeDate = ct.ReminMeDate
                                                                  }).ToList() : null
                            }).ToList();

            //if (response is null)
            //    return new ErrorDataResult<List<Category>>(HttpStatusCode.NotFound, "NotFound");

            return new PagingResult<Category>(response, query.TotalItemCount, query.Success, query.Message);
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<Category>> GetByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
                return new ErrorDataResult<Category>(null, Messages.InvalidCategoryId);

            var categories = await _categoryDal.GetAsync(c => c.CategoryId == categoryId, t => t.Todos).ConfigureAwait(false);

            if (categories is null)
                return new ErrorDataResult<Category>(null, Messages.InvalidCategory);

            Category category = new Category()
            {
                CategoryId = categories.CategoryId,
                Name = categories.Name,
                Todos = categories.Todos != null ? new List<Todo>()
                {
                    new Todo()
                    {
                       CategoryId = categories.Todos.GetListMapped(ct=>ct.CategoryId),
                       Content = categories.Todos.GetListMapped(ct=>ct.Content),
                       DueDate = categories.Todos.GetListMapped(ct=>ct.DueDate),
                       Id = categories.Todos.GetListMapped(ct=>ct.Id),
                       IsFavorite = categories.Todos.GetListMapped(ct=>ct.IsFavorite),
                       ReminMeDate = categories.Todos.GetListMapped(ct=>ct.ReminMeDate),
                    }
                } : null
            };

            return new SuccessDataResult<Category>(category);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        [TransactionScopeAspect]
        public async Task<IResult> UpdateAsync(Category category)
        {
            var updatedCategory = await _categoryDal.GetAsync(i => i.CategoryId == category.CategoryId);

            updatedCategory.Name = category.Name;
            await _categoryDal.UpdateAsync(updatedCategory);

            return new Result(success: true, message: "Kategori Başarıyla Güncellenmiştir.");
        }
    }
}
