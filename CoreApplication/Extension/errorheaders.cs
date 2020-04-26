using Microsoft.AspNetCore.Http;

namespace EzilineApp.Api.CoreApplication.Extension
{
     public static class errorheaders{
     public static void AddApplicationError(this HttpResponse response,string message)
     {  
       response.Headers.Add("Application-Error",message);
       response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
       response.Headers.Add("Access-Control-Allow-Origin","*");
     }}
}