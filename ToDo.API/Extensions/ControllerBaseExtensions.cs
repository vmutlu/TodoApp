using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDo.Core.Utilities.Results;

namespace ToDo.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult GetResult(this ControllerBase controllerBase, IResult result)
        {
            switch (result.StatusCode)
            {
                case (int)HttpStatusCode.Created:
                    {
                        var uri = (controllerBase?.HttpContext?.Request).GetDisplayUrl();
                        return new CreatedResult(uri, result);
                    }
                case (int)HttpStatusCode.NotFound:
                    {
                        return new NotFoundObjectResult(result);
                    }
                case (int)HttpStatusCode.NoContent:
                    {
                        return new NoContentResult();
                    }
                case (int)HttpStatusCode.Forbidden:
                    {
                        return new ForbidResult();
                    }
                case (int)HttpStatusCode.Unauthorized:
                    {
                        return new UnauthorizedResult();
                    }
                case (int)HttpStatusCode.OK:
                    {
                        return new OkObjectResult(result);
                    }
                case 0:
                    {
                        if (result.Success)
                            return new OkObjectResult(result);

                        else
                            return new BadRequestObjectResult(result);
                    }
                default:
                    {
                        return new BadRequestObjectResult(result);
                    }
            }
        }
    }
}
