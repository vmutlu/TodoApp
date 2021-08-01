using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Business.Constants;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Extensions.MapHelper;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;

namespace ToDo.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IUserService _userService;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IUserService userService)
        {
            _operationClaimDal = operationClaimDal;
            _userService = userService;
        }

        [SecuredOperation("admin,user")]
        public async Task<IResult> AddAsync(OperationClaim operation)
        {
            await _operationClaimDal.AddAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessOperationAdded);
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deletedOperation = await _operationClaimDal.GetAsync(i => i.Id == id);
            await _operationClaimDal.DeleteAsync(deletedOperation).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessOperationDeleted);
        }

        [SecuredOperation("admin,user")]
        public async Task<IDataResult<List<OperationClaim>>> GetAllAsync()
        {
            var users = await _userService.GetUsersAsync().ConfigureAwait(false);
            var response = (from oc in await _operationClaimDal.GetAllAsync(null, null, o => o.UserOperationClaims).ConfigureAwait(false)
                            from u in oc.UserOperationClaims
                            from us in users
                            where u.UserId == us.Id
                            select new OperationClaim()
                            {
                                Id = oc.Id,
                                Name = oc.Name,
                                UserOperationClaims = oc.UserOperationClaims != null ? (from uoc in oc.UserOperationClaims
                                                                                        select new UserOperationClaim()
                                                                                        {
                                                                                            User = new User()
                                                                                            {
                                                                                                Id = us.Id,
                                                                                                FirstName = us.FirstName,
                                                                                                LastName = us.LastName,
                                                                                                Email = us.Email,
                                                                                                Status = us.Status
                                                                                            }
                                                                                        }).ToList() : null
                            }).ToList();

            return new SuccessDataResult<List<OperationClaim>>(response);
        }

        [SecuredOperation("admin,user")]
        public async Task<IDataResult<OperationClaim>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return new ErrorDataResult<OperationClaim>(null, Messages.InvalidOperationClaimId);

            var operationClaims = await _operationClaimDal.GetAsync(c => c.Id == id, t => t.UserOperationClaims).ConfigureAwait(false);

            if (operationClaims is null)
                return new ErrorDataResult<OperationClaim>(null, Messages.InvalidOperationClaim);

            var operation = new OperationClaim()
            {
                Id = operationClaims.Id,
                Name = operationClaims.Name,
                UserOperationClaims = operationClaims.UserOperationClaims != null ? new List<UserOperationClaim>()
                                {
                                    new UserOperationClaim()
                                    {
                                        Id = operationClaims.UserOperationClaims.GetListMapped(op => op.Id),
                                        OperationClaimId = operationClaims.UserOperationClaims.GetListMapped(op => op.OperationClaimId),
                                        UserId = operationClaims.UserOperationClaims.GetListMapped(op => op.UserId),
                                        User = operationClaims.UserOperationClaims.GetListMapped(op => op.User)
                                    }
                                } : null
            };

            return new SuccessDataResult<OperationClaim>(operation);
        }

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(OperationClaim operation)
        {
            await _operationClaimDal.UpdateAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessOperationUpdated);
        }
    }
}
