using Microsoft.EntityFrameworkCore;

namespace ControllersAPI.Utils;

public static class HttpContextExtensions
{
    public async static Task InsertPaginationParamsInHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
    {
        if (httpContext is null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        double count = await queryable.CountAsync();
        httpContext.Response.Headers.Append("totalAmountOfRecords", count.ToString());
    }
}
