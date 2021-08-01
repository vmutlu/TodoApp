using System;
using System.Collections.Generic;

namespace ToDo.Core.Utilities.Results
{
    public interface IPaginationResult<out T> : IResult
    {
        IReadOnlyList<T> Data { get; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        Uri FirstPage { get; }
        Uri LastPage { get; }
        int TotalPages { get; }
        int TotalRecords { get; }
        Uri NextPage { get; }
        Uri PreviousPage { get; }
    }
}
