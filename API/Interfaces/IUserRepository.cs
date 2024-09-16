using System;
using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
void Update(AppUser user);

// Task<AppUser?>GetUserByIdAsync(int id);

// Task<PageList<AppUser>>GetAllUserAsync(UserParameters userParameters);
  Task<AppUser?> GetUserById(int id );

Task <AppUser?> GetUserByUserNameAsync(string username);


// the next part is to use the querable extensions 


Task<MemberDto?>GetMemberDtoByIdAsync(int id);

Task<PageList<MemberDto>>GetAllMemebersDtoAsync(UserParameters parameters);


Task <MemberDto?> GetMemberDtoByNameAsync(string username);

}

