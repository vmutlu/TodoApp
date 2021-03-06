using System.Collections.Generic;
using ToDo.Core.Entities.Concrete;

namespace ToDo.Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
