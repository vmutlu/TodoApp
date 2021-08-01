using ToDo.Core.DataAccess.EntityFramework;
using ToDo.DataAccess.Abstract;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Concrete.EntityFramework
{
    public class EfTodoDal : EfEntityRepositoryBase<Todo, TodoContext>, ITodoDal
    {
        public EfTodoDal(TodoContext todoContext) : base(todoContext)
        {

        }
    }
}
