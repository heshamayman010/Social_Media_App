using System.Security.Cryptography;
using System.Text;
using API.Dtos;
using API.Entities;
using API.interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController (AppDbContext context,ITokenService service): BaseApiController
    {

        [HttpPost("register")]
public async Task <ActionResult<UserDto>> Register(RegisterDto dto)
{

    if(await uniquname(dto.username)) return BadRequest("this user name is already taken choose another one");
   
    
//     using var hm =new HMACSHA512();

// var User=new AppUser{
// UserName=dto.username,
// PasswordHash=hm.ComputeHash(Encoding.UTF8.GetBytes(dto.password)),
// PasswordSalt=hm.Key

// };
// context.appUsers.Add(User);
// await context.SaveChangesAsync();
// return new UserDto(){
// UserName=User.UserName,
// Token=service.CreateToken(User)
// };

return Ok();


    }



[HttpPost("login")]
public async Task<ActionResult<UserDto>> Login(LoginDto dto){

var user=await context.appUsers.Include(c=>c.Photos).FirstOrDefaultAsync(x=>x.UserName==dto.username);
if(user==null)return Unauthorized("invalid username");
// here when defining the hamc object we use the password salt from the user data to correctly check for the password 
using var hm=new HMACSHA512(user.PasswordSalt);

var ComputeHash= hm.ComputeHash(Encoding.UTF8.GetBytes(dto.password));

// then we will do for loop for checking for the equality of the new hash and the user passwrod hash

for(int i =0;i<ComputeHash.Length;i++){

if(ComputeHash[i]!=user.PasswordHash[i]) return Unauthorized("Incorrect password try again later ");
}

// now we will return the user dto instead of returning the user      
return new UserDto(){

    UserName=user.UserName,
    Token=service.CreateToken(user),
    photpUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
};

}
// method only used to check if the user name already exists in the database 
private async Task<bool> uniquname(string username){

return await context.appUsers.AnyAsync(x=>x.UserName.ToLower()==username.ToLower());
}

    }
}
