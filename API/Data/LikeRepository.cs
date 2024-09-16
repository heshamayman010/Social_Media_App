using System;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class LikeRepository(AppDbContext context, IMapper mapper) : ILikeRepository
{
    public void AddLike(LikeUser like)
    {

        context.Likes.Add(like);
    }

    public void DeleteLike(LikeUser like)
    {

        context.Likes.Remove(like);
    }

    public async Task<IEnumerable<int>> GetCurrentUserLikeIds(int userid)
    {
        var likedid= await context.Likes.Where(x=>x.SourceUserId==userid)
        .Select(x=>x.TargetUserId).ToListAsync();
        return likedid;
    }


    public async Task<LikeUser?> GetUserlike(int SourceUserId, int TargetUser)
    {
        // find get back the data using the key and here we mad this table keys are the sourceid adn target id 
        return await context.Likes.FindAsync(SourceUserId,TargetUser);

    }

    public async Task<IEnumerable<MemberDto>> GetUserLikess(string predicate, int userid)
    {
// here we will get back all the likes and then apply the query based on the condition 
var query=context.Likes.AsQueryable();
switch (predicate)
{
    case "liked":
    return await query.Where(x=>x.SourceUserId==userid).Select(x=>x.TargetUser).
    ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();


  case "LikedBy":
      return await query.Where(x=>x.TargetUserId==userid).Select(x=>x.SourcUser).
    ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();


default:
    var likedids=await GetCurrentUserLikeIds(userid);
    
    return   await query.Where(x=>x.TargetUserId==userid && likedids.Contains(x.SourceUserId))
                .Select(x=>x.SourcUser).
                 ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
}
    }

}