using System;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(AppDbContext context ,IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<MemberDto>> GetAllMemebersDtoAsync()   // old
    {
        // here we created querable to call only the important props from the data base
        var memberstoreturn= await context.appUsers.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();

        return memberstoreturn;

    }

    public async Task<IEnumerable<AppUser>> GetAllUserAsync()
    {
            var allusers=context.appUsers.Include(x=>x.Photos).ToListAsync();
            return await allusers;
    }

    public async Task<MemberDto?> GetMemberDtoByIdAsync(int id)
    {
        var memberstoreturn= await context.appUsers.Where(x=>x.Id==id).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

        return memberstoreturn;
    }

    public async Task<MemberDto?> GetMemberDtoByNameAsync(string username)
    {
        var memberstoreturn= await context.appUsers.Where(x=>x.UserName==username).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

        return memberstoreturn;
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)//old
    {
        return await context.appUsers.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.Id==id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)//old
    {
        return await context.appUsers.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName==username);
    }

    public async Task<bool> SaveAllAsync()
    {
        return  await context.SaveChangesAsync()>0; 
    }

    public void Update(AppUser user)
    {

        // this will be done latter 

        context.Entry(user).State=EntityState.Modified;
    }
}
