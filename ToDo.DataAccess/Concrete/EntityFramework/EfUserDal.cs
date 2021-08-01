using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.DataAccess.EntityFramework;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;

namespace ToDo.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, TodoContext>, IUserDal
    {
        public EfUserDal(TodoContext todoContext) : base(todoContext)
        {

        }

        public Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using (var context = new TodoContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return Task.FromResult(result.ToList());
            }
        }
    }
}
