using ToDo.Core.DataAccess.EntityFramework;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, TodoContext>, IUserOperationClaimDal
    {
    }
}
