using ToDo.Core.DataAccess.EntityFramework;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, TodoContext>, ICategoryDal
    {
        public EfCategoryDal(TodoContext todoContext) : base(todoContext)
        {

        }
    }
}
