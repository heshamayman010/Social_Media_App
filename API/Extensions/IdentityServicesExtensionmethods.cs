using System;
using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServicesExtensionmethods
{

public static IServiceCollection AddIdentityService(this IServiceCollection service ,IConfiguration config){
// here we will add all the methods for the identity 

service.AddIdentityCore<AppUser>(o=>{
// options for the identity
o.Password.RequireNonAlphanumeric=false;
}).AddRoles<AppRole>().AddRoleManager<RoleManager<AppRole>>()
.AddEntityFrameworkStores<AppDbContext>();



service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{var tokenkey=config["TokenKey"]??throw new Exception("the token key must be found");
options.TokenValidationParameters=new TokenValidationParameters{
//and here you also can validat the issuer of the token 
    ValidateIssuerSigningKey=true,
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
    ValidateAudience=false,
    ValidateIssuer=false
};

// this part is used for the signal r 

options.Events=new JwtBearerEvents{

    OnMessageReceived= context=>{

        var accessToken=context.Request.Query["access_token"];
        var path=context.HttpContext.Request.Path;

        if(!string.IsNullOrEmpty(accessToken)&&path.StartsWithSegments("/hubs")){

            context.Token=accessToken;
        }
        return Task.CompletedTask;
    }
};
});

    return service;
}


}
