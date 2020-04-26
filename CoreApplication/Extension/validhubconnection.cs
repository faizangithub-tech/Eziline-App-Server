using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EzilineApp.Api.CoreApplication.Extension
{
    public static class validhubconnection
    {
       public static void isvalid(this IHttpContextAccessor httpContextAccessor)
       {
           var request=httpContextAccessor.HttpContext.Request;

           if(request.Path.StartsWithSegments("/notify", StringComparison.OrdinalIgnoreCase))
           {
                  request.Query.TryGetValue("access_token", out var accessToken);

                  var token=accessToken[0];
           }
        }
    }
}