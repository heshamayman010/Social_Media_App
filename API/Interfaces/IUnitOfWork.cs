using System;

namespace API.Interfaces;

public interface IUnitOfWork
{

ILikeRepository LikeRepository{get;}

IUserRepository UserRepository{get;}

IMessagesRepository MessagesRepository{get;}

Task<bool> Complete();  // this will be for the save changes of all the entities 

bool HasChanges(); // this will be used to know if the entities are being tracked or not 
}
