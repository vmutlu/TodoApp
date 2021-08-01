using ToDo.Core.DataAccess.EntityFramework;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, TodoContext>, IOperationClaimDal
    {
        public EfOperationClaimDal(TodoContext todoContext) : base(todoContext)
        {

        }
    }
}
