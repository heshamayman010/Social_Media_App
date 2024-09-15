using System.Security.Cryptography;
using System.Text;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController (UserManager<AppUser>userManager,ITokenService service,IMapper mapper): BaseApiController
    {

        [HttpPost("register")]
public async Task <ActionResult<UserDto>> Register(RegisterDto dto)
{

    if(await uniquname(dto.username)) return BadRequest("this user name is already taken choose another one");
var user=mapper.Map<AppUser>(dto);
user.UserName=dto.username.ToLower();
var result=await userManager.CreateAsync(user,dto.password);

if(!result.Succeeded) return BadRequest(result.Errors);

return new UserDto(){
UserName=user.UserName,
Token= await service.CreateToken(user)
,knownas=user.KnownAs
};
    }



[HttpPost("login")]
public async Task<ActionResult<UserDto>> Login(LoginDto dto){

var user=await userManager.Users.
Include(c=>c.Photos).
FirstOrDefaultAsync(x=>x.NormalizedUserName==dto.username.ToUpper());  // here the normalized user name return all the chars in capital case


if(user==null||user.UserName==null)return Unauthorized("invalid username");

// now to check for hte password 
var passwordcorrect=await userManager.CheckPasswordAsync(user,dto.password);

if(!passwordcorrect) return BadRequest("the password you enterd is not correct");

return new UserDto(){

    UserName=user.UserName,
    Token=await service.CreateToken(user),
    photpUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
    ,knownas=user.KnownAs
};

}
// method only used to check if the user name already exists in the database 
private async Task<bool> uniquname(string username){

return await userManager.Users.AnyAsync(x=>x.NormalizedUserName==username.ToUpper());
}

    }
}
