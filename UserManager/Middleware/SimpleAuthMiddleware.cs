using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Tools;

namespace UserManager.Middleware
{
    public class SimpleAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _nonAuthPaths = new List<string> { "/api/User/Register", "/api/User/Login" };

        public SimpleAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Skipping token validation for register and login actions
            var path = context.Request.Path;
            if (_nonAuthPaths.Contains(path))
            {
                await _next(context);
                return;
            }
                
            string authHeader = context.Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value;
            if(authHeader == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header is missing");
                return;
            }

            var isValid = TokenGenerator.Validate(authHeader);
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(context);
        }
    }

    public static class SimpleAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseSimpleAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleAuthMiddleware>();
        }
    }
}
