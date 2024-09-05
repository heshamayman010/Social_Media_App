using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.interfaces;
using API.Interfaces;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    public class UserController(IUserRepository userrepo ) : BaseApiController
    {

    [HttpGet]
    public async Task<IActionResult> Getusers(){

      //   var users= await userrepo.GetAllUserAsync();

      //   var userstoreturn=mapper.Map<IEnumerable<MemberDto>>(users); // the old way 
     var  userstoreturn=await userrepo.GetAllMemebersDtoAsync();
        return Ok(userstoreturn);

    }

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




    }






}
