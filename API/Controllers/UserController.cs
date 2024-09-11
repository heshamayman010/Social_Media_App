using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.interfaces;
using API.Interfaces;
using AutoMapper;
using API.Dtos;
using System.Security.Claims;
using API.Extensions;
using API.Helpers;

namespace API.Controllers
{
    public class UserController(IUserRepository userrepo ,IMapper mapper,IPhotoService photoService) : BaseApiController
    {
[Authorize]
    [HttpGet]
    public async Task<IActionResult> Getusers([FromQuery]UserParameters parameters){

        // now we want also to send the user naem of the current user with the params 
        parameters.currenusername=User.GetUsername();

      //   var userstoreturn=mapper.Map<IEnumerable<MemberDto>>(users); // the old way 
     var  userstoreturn=await userrepo.GetAllMemebersDtoAsync(parameters);
     Response.AddPaginationHeader(userstoreturn);
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

// var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
// this was the old way before using the claims extension method in the user 
// if(username==null)return BadRequest("the user name is nullllll ");

var myuser=await userrepo.GetUserByUserNameAsync(User.GetUsername());

if(myuser==null)return BadRequest("cant find this user ");

// when using this approach or overload here it will update the myusre and set the values from the memberdto 
mapper.Map(membertoupdate,myuser);


// nocontent is for the 204 status code 
if(await userrepo.SaveAllAsync()) return NoContent();

return BadRequest("cant update this user data or maybe no data changed  ");
}




[HttpPost("add-photo")]
public  async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file){

var username=User.GetUsername();
var user= await userrepo.GetUserByUserNameAsync(username);
if(user==null) return BadRequest("cant upload this photo to the data base ");

// then the function to upload the photo 

var result=await photoService.AddPhoto(file);
if(result.Error!=null) return BadRequest(result.Error.Message);


var photo=new Photo{


    Url=result.SecureUrl.AbsoluteUri,

PublicId=result.PublicId
};

user.Photos.Add(photo);

if(await userrepo.SaveAllAsync())

return CreatedAtAction(nameof(Getuser),new{username=user.UserName},mapper.Map<PhotoDto>(photo));  // it is the same as the old way using the url

return BadRequest("cant upload the photoooo"); 



}




[HttpPut("set-main-photo/{photoid}")]
public async Task<IActionResult> SetMainphot(int photoid ){

var user= await userrepo.GetUserByUserNameAsync(User.GetUsername());
if(user==null) return BadRequest("cant find this user ");

var thechoosedphoto=user.Photos.FirstOrDefault(x=>x.Id==photoid);
if(thechoosedphoto==null||thechoosedphoto.IsMain)return BadRequest("cant use this photo as main photo ");

var oldmainphoto=user.Photos.FirstOrDefault(x=>x.IsMain);

if(oldmainphoto!=null) oldmainphoto.IsMain=false;

thechoosedphoto.IsMain=true;
if(await userrepo.SaveAllAsync())return NoContent()
;

return BadRequest("updating the data base failed ");
}




[HttpDelete("delete-photo/{photoid:int}")]
public async Task <IActionResult> Deletephoto(int photoid){

var user=await userrepo.GetUserByUserNameAsync(User.GetUsername());

if(user==null) return BadRequest("this user cant be found  this user name is not for any user  ");

var photo=user.Photos.FirstOrDefault(x=>x.Id==photoid);
if(photo==null||photo.IsMain)return BadRequest("this photo cant be found in the user photos or it is the main photo  ");

// now we will remove the photo but we had to remove it from the cloudinary and also the database 

if(photo.PublicId!=null){
var result= await photoService.DeletePhoto(photo.PublicId);
if (result.Error !=null) return BadRequest("this photo couldnt be deleted from the cloud ");
}
// then at the data base 
user.Photos.Remove(photo);
if(await userrepo.SaveAllAsync()) return Ok( );

return BadRequest("error has occured while updating the data base  ");

}

    }

}
