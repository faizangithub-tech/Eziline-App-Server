
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class BroadCast:Hub
{
     public Task BroadCastServer (string message)
     {

           return Clients.All.SendAsync("SendMessage",message);

     }
     public override Task OnConnectedAsync()
     {
             return base.OnConnectedAsync();
     }

    public override  Task OnDisconnectedAsync(Exception exception)
    {
            return base.OnDisconnectedAsync(exception);
    }
}