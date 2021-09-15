using System.Collections.Generic;

namespace ToDo.Core.Utilities.Results
{
    public interface IPagingResult<T> : IResult
    {
        List<T> Data { get; }
        int TotalItemCount { get; }
    }
}
