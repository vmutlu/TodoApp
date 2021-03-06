using ToDo.Core.DataAccess;
using ToDo.Entities.Concrate;

namespace ToDo.DataAccess.Abstract
{
    public interface ITodoDal : IEntityRepository<Todo>
    {
    }
}
