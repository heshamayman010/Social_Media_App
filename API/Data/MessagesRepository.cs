using System;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MessagesRepository(AppDbContext context,IMapper mapper) : IMessagesRepository
{
    public void AddMessage(Messages messages)
    {
        context.messages.Add(messages);
    }

    public void DeleteMessage(Messages messages)
    {
        context.messages.Remove(messages);
    }

    public async Task<Messages?> GetMessage(int id)
    {
        return await context.messages.FindAsync(id);
    }




    public async Task<PageList<MessagesDto>> GetMessagesForUser(MessagesParams messagesParams)
    {

        var query=context.messages.OrderByDescending(x=>x.MessageSent).AsQueryable();

        // and now we will make swith for the type of the messages based on the sender user name and the params we got 

    query=messagesParams.Container switch{
        "inBox"=>query.Where(x=>x.RecipientUserName==messagesParams.username&& x.RecipientDeleteted==false) ,
        "outBox"=>query.Where(x=>x.SenderUserName==messagesParams.username && x.SenderDeleteted==false),
        //and for the defauls 
        // _ =>query.Where(x=>x.RecipientUserName==messagesParams.username &&x.DateReadd==null)
        _ =>query.Where(x=>x.RecipientUserName==messagesParams.username && x.RecipientDeleteted==false )
    };
var messages=query.ProjectTo<MessagesDto>(mapper.ConfigurationProvider);

return await PageList<MessagesDto>.CreateAsync(messages,messagesParams.pagenumber,messagesParams.pagesize);
    }




    public async Task<IEnumerable<MessagesDto>> GetMessagesThread(string CurrentUsername, string recipientUserName)
    {
        var messages=await context.messages.Include(x=>x.Sender).ThenInclude(x=>x.Photos)
        .Include(c=>c.Recipient).ThenInclude(c=>c.Photos)
        .Where(x=>
        x.RecipientUserName==CurrentUsername&& x.RecipientDeleteted==false && x.SenderUserName==recipientUserName||
        x.SenderUserName==CurrentUsername&& x.SenderDeleteted==false&&x.RecipientUserName==recipientUserName    // here we make it for the both conditions to get all the messages between the two users 
        ).OrderBy(c=>c.MessageSent).ToListAsync();


        // and then we want to get the list of all the unread messages using the date read is null

        var unreadmessages= messages.Where(x=>x.DateReadd==null&&x.RecipientUserName==CurrentUsername).ToList();

    // and now we will make the date read of them as now 

    if (unreadmessages.Count!=0){
       unreadmessages.ForEach(x =>x.DateReadd=DateTime.UtcNow);

            await context.SaveChangesAsync();
                                 }
// then return all the messages dtos 
return mapper.Map<IEnumerable<MessagesDto>>(messages);
    }
   
   
}
        
