using System.Net;

namespace ToDo.Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(HttpStatusCode statusCode) =>
            StatusCode = (int)statusCode;

        public Result(bool success, string message) : this(success) =>
            Message = message;

        public Result(bool success) =>
            Success = success;

        public Result(HttpStatusCode statusCode, string message) : this(statusCode) =>
            Message = message;

        public bool Success { get; }

        public string Message { get; }

        public int StatusCode { get; }
    }
}
