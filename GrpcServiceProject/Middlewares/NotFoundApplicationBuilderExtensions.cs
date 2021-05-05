using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;


namespace GrpcServiceProject.Middlewares
{
    #region Non-IMiddleware
    /*
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;
        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync(@"( *^-^)Not found");
                    }
        }
    }*/
    public class NotFoundMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
            await next(context);
            if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync(@"( *^-^)Not found");
            }
        }
    }
    #endregion


    public static class NotFoundMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotFound(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NotFoundMiddleware>();
        }
    }
}
