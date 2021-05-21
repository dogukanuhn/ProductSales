using Microsoft.AspNetCore.Builder;

namespace ProductSales.API.Helpers.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }


    }
}
