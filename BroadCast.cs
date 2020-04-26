
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using EzilineApp.Api.CoreApplication.Extension;

public class BroadCast:Hub
{
    
      private readonly IHttpContextAccessor _httpContextAccessor;
      public BroadCast(IHttpContextAccessor httpContextAccessor)
      {
        _httpContextAccessor = httpContextAccessor;
      }
     public Task BroadCastServer (string message)
     {

           return Clients.All.SendAsync("SendMessage",message);

     }

     public override Task OnConnectedAsync()
     {
             validhubconnection.isvalid(_httpContextAccessor);

             return base.OnConnectedAsync();
     }

     public override  Task OnDisconnectedAsync(Exception exception)
     {
            return base.OnDisconnectedAsync(exception);
     }

    
}