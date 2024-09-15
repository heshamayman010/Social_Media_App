using System;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(AppDbContext context ,IMapper mapper) : IUserRepository
{
    public async Task<PageList<MemberDto>> GetAllMemebersDtoAsync(UserParameters parameters) 
    {
        // here we created querable to call only the important props from the data base 
        var query=  context.Users.AsQueryable();

// query=        parameters.OrderBy switch 
//         {
//             "created" =>query.OrderByDescending(x=>x.Created),
//             _ =>query.OrderByDescending(x=>x.LAstActiv)
//         };

        query=query.Where(x=>x.UserName!=parameters.currenusername);
        return await PageList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(mapper.ConfigurationProvider),parameters.pagenumber,parameters.pagesize);

       
    }


    public async Task<MemberDto?> GetMemberDtoByIdAsync(int id)
    {
        var memberstoreturn= await context.Users.Where(x=>x.Id==id).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

        return memberstoreturn;
    }

    public async Task<MemberDto?> GetMemberDtoByNameAsync(string username)
    {
        var memberstoreturn= await context.Users.Where(x=>x.UserName==username).ProjectTo<MemberDto>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

        return memberstoreturn;
    }


    public async Task<AppUser?> GetUserByUserNameAsync(string username)//old
    {
        return await context.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.UserName==username);
    }


    public async Task<AppUser?> GetUserById(int id )//old
    {
        return await context.Users.Include(x=>x.Photos).FirstOrDefaultAsync(x=>x.Id==id);
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


// public string Getusernamebyitsid(int id ){

// var myusr=context.appUsers.FirstOrDefault(x=>x.Id==id) ;
//     return myusr.UserName;

// }


}
