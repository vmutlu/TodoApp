using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Business.ValidationRules.FluentValidation;
using ToDo.Core.Aspects.Autofac.Caching;
using ToDo.Core.Aspects.Autofac.Transaction;
using ToDo.Core.Aspects.Autofac.Validation;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> AddAsync(Category category)
        {
            await _categoryDal.AddAsync(category);

            return new Result(success: true, message: "Ürün Başarıyla Eklenmiştir.");
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleted = await _categoryDal.GetAsync(i=>i.CategoryId == id);
            await _categoryDal.DeleteAsync(deleted);

            return new Result(success: true, message: "Ürün Başarıyla Eklenmiştir.");
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<List<Category>>> GetAllAsync()
        {
            return new SuccessDataResult<List<Category>>(await _categoryDal.GetAllAsync());
        }

        [SecuredOperation("admin,user")]
        [CacheAspect]
        public async Task<IDataResult<Category>> GetByIdAsync(int categoryId)
        {
            return new SuccessDataResult<Category>(await _categoryDal.GetAsync(c => c.CategoryId == categoryId));
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        [TransactionScopeAspect]
        public async Task<IResult> UpdateAsync(Category category)
        {
            var updatedCategory = await _categoryDal.GetAsync(i=>i.CategoryId == category.CategoryId);

            updatedCategory.Name = category.Name;
            await _categoryDal.UpdateAsync(updatedCategory);

            return new Result(success: true, message: "Ürün Başarıyla Eklenmiştir.");
        }
    }
}
