using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Constants;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;

namespace ToDo.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal) => _userOperationClaimDal = userOperationClaimDal;

        public async Task<IResult> AddAsync(UserOperationClaim operation)
        {
            await _userOperationClaimDal.AddAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessUserOperationAdded);
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var deleting = await _userOperationClaimDal.GetAsync(uo => uo.Id == id).ConfigureAwait(false);
            if (deleting is null)
                return new ErrorResult(Messages.InvalidOperationClaimId);

            await _userOperationClaimDal.DeleteAsync(deleting).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessUserOperationDeleted);
        }

        public async Task<IDataResult<List<UserOperationClaim>>> GetAllAsync()
        {
            var response = (from uoc in await _userOperationClaimDal.GetAllAsync(null, o => o.User, o => o.OperationClaim).ConfigureAwait(false)
                            select new UserOperationClaim()
                            {
                                Id = uoc.Id,
                                UserId = uoc.UserId,
                                OperationClaimId = uoc.OperationClaimId,
                                User = uoc.User != null ? new User()
                                {
                                    FirstName = uoc.User.FirstName,
                                    LastName = uoc.User.LastName,
                                    Email = uoc.User.Email,
                                    Status = uoc.User.Status
                                } : null,
                                OperationClaim = uoc.OperationClaim != null ? new OperationClaim()
                                {
                                    Name = uoc.OperationClaim.Name
                                } : null
                            }).ToList();

            return new SuccessDataResult<List<UserOperationClaim>>(response);
        }

        public async Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<UserOperationClaim>(null, Messages.InvalidOperationClaimId);

            var operationClaims = await _userOperationClaimDal.GetAsync(c => c.Id == id, t => t.User, t => t.OperationClaim).ConfigureAwait(false);

            if (operationClaims is null)
                return new ErrorDataResult<UserOperationClaim>(null, Messages.InvalidOperationClaim);

            var operation = new UserOperationClaim()
            {
                Id = operationClaims.Id,
                UserId = operationClaims.UserId,
                OperationClaimId = operationClaims.OperationClaimId,
                User = operationClaims.User != null ? new User()
                {
                    LastName = operationClaims.User.LastName,
                    FirstName = operationClaims.User.FirstName,
                    Email = operationClaims.User.Email,
                    Status = operationClaims.User.Status
                } : null,
                OperationClaim = operationClaims.OperationClaim != null ? new OperationClaim()
                {
                    Name = operationClaims.OperationClaim.Name
                } : null
            };

            return new SuccessDataResult<UserOperationClaim>(operation);
        }

        public async Task<IResult> UpdateAsync(UserOperationClaim operation)
        {
            await _userOperationClaimDal.UpdateAsync(operation).ConfigureAwait(false);
            return new Result(success: true, Messages.SuccessUserOperationUpdated);
        }
    }
}
