using System;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public class UnitOfWork (AppDbContext context,IUserRepository userRepository,IMessagesRepository messagesRepository
,ILikeRepository likeRepository) : IUnitOfWork
{
    public ILikeRepository LikeRepository => likeRepository;

    public IUserRepository UserRepository => userRepository;

    public IMessagesRepository MessagesRepository => messagesRepository;

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync()>0;
    }

    public  bool HasChanges()
    {
        return  context.ChangeTracker.HasChanges();
    }
}
