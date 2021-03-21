using FluentValidation.Results;
using System.Collections.Generic;

namespace ToDo.Core.Extensions
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
