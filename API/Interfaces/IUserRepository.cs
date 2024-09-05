using System;
using API.Dtos;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
void Update(AppUser user);
Task<bool>SaveAllAsync();

Task<AppUser?>GetUserByIdAsync(int id);

Task<IEnumerable<AppUser>>GetAllUserAsync();


Task <AppUser?> GetUserByUserNameAsync(string username);


// the next part is to use the querable extensions 


Task<MemberDto?>GetMemberDtoByIdAsync(int id);

Task<IEnumerable<MemberDto>>GetAllMemebersDtoAsync();


Task <MemberDto?> GetMemberDtoByNameAsync(string username);

}

