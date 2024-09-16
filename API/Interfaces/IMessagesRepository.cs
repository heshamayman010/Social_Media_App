using System;
using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IMessagesRepository
{

void AddMessage(Messages message);
void DeleteMessage(Messages message);

Task<Messages?>GetMessage(int id );

Task<PageList<MessagesDto>>GetMessagesForUser(MessagesParams messagesParams);

Task<IEnumerable<MessagesDto>> GetMessagesThread(string CurrentUsername,string RecipientUserName);






}
