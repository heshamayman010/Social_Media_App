using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]  // this will be called localhost:port/api/User
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext context = context;

    [HttpGet]
    public IActionResult Getusers(){

        var users=context.appUsers.ToList();
        return Ok(users);

    }


    [HttpGet("{id}")]
    public IActionResult Getuser(int id){

            // find searches using the key values but first or default uses the predicate s
         var user=context.appUsers.Find(id);
         // and for checking for the null values 
         if(user==null){

            return BadRequest();
         }else{

        return Ok(user);
         }
    }




    }






}
