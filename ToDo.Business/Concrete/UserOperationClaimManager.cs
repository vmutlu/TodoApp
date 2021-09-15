using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.BusinessAspects.Autofac;
using ToDo.Business.Constants;
using ToDo.Core.Entities;
using ToDo.Core.Entities.Concrete;
using ToDo.Core.Extensions;
using ToDo.Core.Services.Abstract;
using ToDo.Core.Utilities.Results;
using ToDo.DataAccess.Abstract;

namespace ToDo.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IPaginationUriService _uriService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IPaginationUriService uriService) { _userOperationClaimDal = userOperationClaimDal;  _uriService = uriService; }

        public async Task<IResult> AddAsync(UserOperationClaim operation)
        {
            await _userOperationClaimDal.AddAsync(operation).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessUserOperationAdded);
        }

        [SecuredOperation("admin")]
        public async Task<IResult> DeleteAsync(int id)
        {
            var deleting = await _userOperationClaimDal.GetAsync(uo => uo.Id == id).ConfigureAwait(false);
            if (deleting is null)
                return new ErrorResult(Messages.InvalidOperationClaimId);

            await _userOperationClaimDal.DeleteAsync(deleting).ConfigureAwait(false);
            return new Result(success: true, message: Messages.SuccessUserOperationDeleted);
        }

        [SecuredOperation("admin")]
        public async Task<PagingResult<UserOperationClaim>> GetAllAsync(GeneralFilter generalFilter = null)
        {
            var query = await _userOperationClaimDal.GetAllForPagingAsync(generalFilter.Page, generalFilter.PropertyName, generalFilter.Asc, null, o => o.User, o => o.OperationClaim).ConfigureAwait(false);

            var response = (from uoc in query.Data
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

            return new PagingResult<UserOperationClaim>(response, query.TotalItemCount, query.Success, query.Message);
        }

        [SecuredOperation("admin")]
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

        [SecuredOperation("admin")]
        public async Task<IResult> UpdateAsync(UserOperationClaim operation)
        {
            await _userOperationClaimDal.UpdateAsync(operation).ConfigureAwait(false);
            return new Result(success: true, Messages.SuccessUserOperationUpdated);
        }
    }
}
