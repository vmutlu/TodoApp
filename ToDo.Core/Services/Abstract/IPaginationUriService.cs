using System;
using ToDo.Core.Utilities.Results;

namespace ToDo.Core.Services.Abstract
{
    public interface IPaginationUriService
    {
        public Uri GetPageUri(PaginationQuery paginationQuery);
    }
}
