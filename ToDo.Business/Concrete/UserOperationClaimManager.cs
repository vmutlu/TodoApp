using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Entities.Concrete;
using ToDo.DataAccess.Abstract;

namespace ToDo.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal) => _userOperationClaimDal = userOperationClaimDal;

        public async Task AddAsync(UserOperationClaim tEntity)
        {
            var userOperationClaim = new UserOperationClaim()
            {
                OperationClaimId = tEntity.OperationClaimId,
                UserId = tEntity.UserId
            };

            await _userOperationClaimDal.AddAsync(userOperationClaim);
        }
    }
}
