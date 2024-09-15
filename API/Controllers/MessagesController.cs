using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController(IMessagesRepository messagesrepo,IUserRepository userrepo,IMapper mapper)
     : BaseApiController
    {

[HttpPost]
public async Task<ActionResult<MessagesDto>>CreateMessage(CreateMessagesDto messagesDtocreate){

    var username=User.GetUsername();
   
   
    if(username==messagesDtocreate.RecipientUsername)return   // here you cant send messages to your self 
                            BadRequest("cant send messages to yourself");

var sender=await userrepo.GetUserByUserNameAsync(username);
var recipient=await userrepo.GetUserByUserNameAsync(messagesDtocreate.RecipientUsername);
if(sender==null || recipient==null) return BadRequest("cant send messages to null users ");


var message=new Messages{
Sender=sender,
Recipient=recipient,
Content=messagesDtocreate.Content,
SenderUserName=sender.UserName,
RecipientUserName=recipient.UserName
};
messagesrepo.AddMessage(message);

if(await messagesrepo.SaveAllAsync())
{
return Ok(mapper.Map<MessagesDto>(message));
}
return BadRequest("failed to save the MEssages at the data base ");
}


[HttpGet]
public async Task<ActionResult<IEnumerable<MessagesDto>>> GetMessagesForUsers([FromQuery]MessagesParams messagesParams)
{
    // we will only specify this propery as it will be from the user token (usernsame)
messagesParams.username=User.GetUsername();
var messages=await messagesrepo.GetMessagesForUser(messagesParams);

Response.AddPaginationHeader(messages);


return messages;

}




// here the username paramter is the name of the other usr we want to sea the threads between us 
[HttpGet("thread/{username}")]
public async Task<ActionResult<IEnumerable<MessagesDto>>> GetCurrentUserthreads(string username){

var currentusername=User.GetUsername();

var messagestoreturn=await messagesrepo.GetMessagesThread(currentusername,username);

return Ok(messagestoreturn);
}


[HttpDelete("{id}")]

public async Task<ActionResult>DeletePhoto(int id )
{
var username=User.GetUsername();

var messagetodelete=await messagesrepo.GetMessage(id);

if(messagetodelete==null)return BadRequest("cant delete this message");

if(messagetodelete.SenderUserName==username)messagetodelete.SenderDeleteted=true;
if(messagetodelete.RecipientUserName==username)messagetodelete.RecipientDeleteted=true;

if(messagetodelete is {SenderDeleteted:true,SenderDeleteted:true}){

    messagesrepo.DeleteMessage(messagetodelete);
}


if(await messagesrepo.SaveAllAsync())return Ok();

return BadRequest("error with deleting the message");
}

    }
}
