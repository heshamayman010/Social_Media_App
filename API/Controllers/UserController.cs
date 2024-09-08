using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.interfaces;
using API.Interfaces;
using AutoMapper;
using API.Dtos;
using System.Security.Claims;

namespace API.Controllers
{
    public class UserController(IUserRepository userrepo ,IMapper mapper) : BaseApiController
    {
[Authorize]
    [HttpGet]
    public async Task<IActionResult> Getusers(){

      //   var users= await userrepo.GetAllUserAsync();

      //   var userstoreturn=mapper.Map<IEnumerable<MemberDto>>(users); // the old way 
     var  userstoreturn=await userrepo.GetAllMemebersDtoAsync();
        return Ok(userstoreturn);

    }
[Authorize]

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Getuser(int id){

         // var user= await userrepo.GetUserByIdAsync(id) ; // old one 

         var user =await userrepo.GetMemberDtoByIdAsync(id);
         if(user==null){

            return BadRequest("No userwith this id found ");

         }else{
            // var usertoreurn=mapper.Map<MemberDto>(user);   // old

            return Ok(user);
         }
    }

[Authorize]

    [HttpGet("{username}")]
    public async Task<IActionResult> Getuser(string username){

         var user= await userrepo.GetMemberDtoByNameAsync(username) ;
         if(user==null){
            return BadRequest("No user with this name found ");
         }
         else
         {

            // var usertoreurn=mapper.Map<MemberDto>(user);

            return Ok(user);
         }
    }


[Authorize]

[HttpPut]
public async Task<IActionResult> Update(MemeberUpdateDto membertoupdate){

// here we will use the name of the user from the claims to get his data back instead of using the route or query params as it 
// can lead to idor valuerablitiy 

var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

if(username==null)return BadRequest("the user name is nullllll ");

var myuser=await userrepo.GetUserByUserNameAsync(username);

if(myuser==null)return BadRequest("cant find this user ");

// when using this approach or overload here it will update the myusre and set the values from the memberdto 
mapper.Map(membertoupdate,myuser);


// nocontent is for the 204 status code 
if(await userrepo.SaveAllAsync()) return NoContent();

return BadRequest("cant update this user data or maybe no data changed  ");
}



    }






}
