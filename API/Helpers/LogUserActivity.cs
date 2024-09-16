using System;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers;
// this class will be used to change the last active for each user in the site when do any thing 
public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
            // all we write here will be done before the action excution 
        var resultcontext=await next();
    // all we write here will be done after the action excution 

if(context.HttpContext.User.Identity?.IsAuthenticated !=true)return; // to check if the user is logged in 

var id=resultcontext.HttpContext.User.GetUserid();
// here we will get back the user repor from the service container 
var repo=resultcontext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
var user=await repo.UserRepository.GetUserById(id);
if(user ==null)return;
user.LAstActiv=DateTime.UtcNow;
await repo.Complete();

    }
}

