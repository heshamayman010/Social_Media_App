using System;
using API.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace API.TheSignalR;

public class PresenceHub (PresenceTracker tracker):Hub
{
    public override async Task OnConnectedAsync()
    {

        if(Context.User==null) throw new HubException("cant get this current username");
      await tracker.Userconnected(Context.User.GetUsername(),Context.ConnectionId);
        // this will call the message in the front end 
      await Clients.Others.SendAsync("UserIsOnline",Context.User?.GetUsername());

    var currentusers= await tracker.GetAlltheOnlineUsers();
    // then we will send to all the client 
    await Clients.All.SendAsync("GetOnLineUsers",currentusers);


        }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {

        if(Context.User==null) throw new HubException("cant get this current username");

        await tracker.UserDisconnected(Context.User.GetUsername(),Context.ConnectionId);
        await Clients.Others.SendAsync("UserIsOffline",Context.User?.GetUsername());
        var currentusers= await tracker.GetAlltheOnlineUsers();
     // then we will send to all the client 
         await Clients.All.SendAsync("GetOnLineUsers",currentusers);

        await base.OnDisconnectedAsync(exception);
    }
}
