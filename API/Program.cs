using System.Text;
using API;
using API.Entities;
using API.interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using API.Extensions;
using API.Middlewares;
using API.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// the next service is instead of the add cors and controllers and also for the adddbcontext 
builder.Services.addApplicationService(builder.Configuration);

// and here is the part of the identity for including the token and all it's functionality 
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200","https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// this part is only used when adding the seed data 

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try
{
    var context=services.GetRequiredService<AppDbContext>();
    var usermanger=services.GetRequiredService<UserManager<AppUser>>();
    var roleManager=services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(usermanger,roleManager);
}
catch (Exception ex )
{

// here we will sea the error at the logger 

var log=services.GetRequiredService<ILogger<Program>>();
log.LogError(ex,"there is error in the code ");

}
app.Run();
