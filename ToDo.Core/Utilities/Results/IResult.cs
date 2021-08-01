namespace ToDo.Core.Utilities.Results
{
    public interface IResult
    {
        int StatusCode { get; }
        bool Success { get; }
        string Message { get; }
    }
}
