using System;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.TheSignalR;

public class MessagesHub(IMessagesRepository messagesrepo,IUserRepository userrepo,IMapper mapper):Hub
{
    public override async Task OnConnectedAsync()
    {

            var httpcontext=Context.GetHttpContext();
            var otheruser=httpcontext?.Request.Query["user"];

if(Context.User==null||string.IsNullOrEmpty(otheruser))throw new Exception("cant create this connection now");
    var groupname=getgroupname(Context.User.GetUsername(),otheruser);

    // then create the group 
    await Groups.AddToGroupAsync(Context.ConnectionId,groupname);

   // and to get the messages thread 

   var messages=messagesrepo.GetMessagesThread(Context.User.GetUsername(),otheruser!);
   
await Clients.Group(groupname).SendAsync("RecieveMessage",messages);

    }




    public override  Task OnDisconnectedAsync(Exception? exception)
    {
         return  base.OnDisconnectedAsync(exception);
    }





// this method will take all the functionality from the controller 
public async Task SendMessage(CreateMessagesDto createMessagesDto){
 var username=Context.User?.GetUsername()??throw new Exception("couldnt get the user ");
   
   
    if(username==createMessagesDto.RecipientUsername)   // here you cant send messages to your self 
                           throw new HubException("you cant send messages to yourself");
var sender=await userrepo.GetUserByUserNameAsync(username);
var recipient=await userrepo.GetUserByUserNameAsync(createMessagesDto.RecipientUsername);
if(sender==null || recipient==null) throw new Exception("cant send messages to null users ");


var message=new Messages{
Sender=sender,
Recipient=recipient,
Content=createMessagesDto.Content,
SenderUserName=sender.UserName,
RecipientUserName=recipient.UserName
};
messagesrepo.AddMessage(message);

if(await messagesrepo.SaveAllAsync())
{

var datatosend=mapper.Map<MessagesDto>(message);
var group=getgroupname(Context.User.GetUsername(),createMessagesDto.RecipientUsername);
// then call method in the client for the group
await Clients.Group(group).SendAsync("NewMessage",datatosend);
}

}


private string getgroupname(string caller,string? otheruser){

var stringcomapre=string.CompareOrdinal(caller,otheruser)<0;

return stringcomapre?$"{caller}-{otheruser}":$"{otheruser}-{caller}";

}

}
