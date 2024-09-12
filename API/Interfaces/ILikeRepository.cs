
using System;
using API.Dtos;
using API.Entities;
using CloudinaryDotNet.Actions;

namespace API.Interfaces;
public interface ILikeRepository{

 Task<LikeUser?>GetUserlike(int SourceUserId,int TargetUser);

 // and this will be general one to get the users under conditions 
 Task<IEnumerable<MemberDto>>GetUserLikess(string predicate,int userid);


// the next one will be used to display the id of the users that had liked this user 
Task<IEnumerable<int>> GetCurrentUserLikeIds(int userid );

void AddLike(LikeUser like);
void DeleteLike(LikeUser like);


Task<bool> SaveChanges();
}