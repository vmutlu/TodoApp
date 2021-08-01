using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using ToDo.Core.Extensions;
using ToDo.Core.Services.Abstract;
using ToDo.Core.Utilities.Results;

namespace ToDo.Core.Services.Concrete
{
    public class PaginationUriService : IPaginationUriService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaginationUriService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetPageUri(PaginationQuery paginationQuery)
        {
            var baseUri = _httpContextAccessor.GetRequestUri();
            var route = _httpContextAccessor.GetRoute();

            var endpoint = new Uri(string.Concat(baseUri, route));
            var queryUri = QueryHelpers.AddQueryString($"{endpoint}", "pageNumber", $"{paginationQuery.PageNumber}");

            queryUri = QueryHelpers.AddQueryString(queryUri, "pageSize", $"{paginationQuery.PageSize}");
            return new Uri(queryUri);
        }
    }
}
