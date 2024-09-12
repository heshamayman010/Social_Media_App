using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController(ILikeRepository repo) : BaseApiController
    {
[HttpPost("{targetuserid}")]
public async Task<IActionResult> ToggleLike(int targetuserid){

//get the usr id 
var sourceuserid=User.GetUserid();
if(sourceuserid==targetuserid) return BadRequest("cant make like here ");  // here the user can make like on his photo 

// and then we will check if there is teh same like in the data base 

var oldlike=await repo.GetUserlike(sourceuserid,targetuserid);

if (oldlike==null){

    var newlike= new  LikeUser{
SourceUserId=sourceuserid,
TargetUserId=targetuserid
    };


repo.AddLike(newlike);
}


else{     // here that means the user press like on photo that he liked before so we will delete it 
 repo.DeleteLike(oldlike);
}
if(await repo.SaveChanges())return Ok();
return BadRequest("cant add this like to the data base");

}





[HttpGet("list")]
// public async Task <ActionResult<IEnumerable<int>> GetcurrentUserlikedIds(){
public async Task <IActionResult> GetcurrentUserlikedIds(){

var sourceuserid=User.GetUserid();
return Ok(await repo.GetCurrentUserLikeIds(sourceuserid));
} 

[HttpGet]
public async Task<IActionResult> GetuserLiked(string predicate){

var users= await repo.GetUserLikess(predicate,User.GetUserid());

return Ok(users);
}

}
}
