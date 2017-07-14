using Ivy.Auth0.Web.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ivy.Auth0.Web.Middleware
{
    public class JsonWebTokenAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public JsonWebTokenAuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var contextProcessor = (IAuth0ContextProcessor)context.RequestServices.GetService(typeof(IAuth0ContextProcessor));

            await contextProcessor.ProcessContextAsync(context);

            await next(context);
        }
    }

    public static class JsonWebTokenAuthorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseJsonWebTokenAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsonWebTokenAuthorizationMiddleware>();
        }
    }
}