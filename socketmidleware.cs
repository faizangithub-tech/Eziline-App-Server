

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AdminService.Middleware
{
   
    public class socketmidleware
    {
             private readonly RequestDelegate _next;

             public socketmidleware(RequestDelegate next)
             {
                 _next=next;
             }

             public async Task Invoke(HttpContext httpContext)
             {
                var request=httpContext.Request;

                if (request.Path.StartsWithSegments("/notify", StringComparison.OrdinalIgnoreCase) && request.Query.TryGetValue("access_token", out var accessToken))
                {
                   Console.WriteLine("Here Iam in sockets middleware??!!");
                   var token=accessToken[0];
                   
                   request.Headers.Add("Authorization", $"Bearer {token}");
                   
                }

                await _next(httpContext);
             }
    }
}