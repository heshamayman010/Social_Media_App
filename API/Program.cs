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
var builder = WebApplication.CreateBuilder(args);

// the next service is instead of the add cors and controllers and also for the adddbcontext 
builder.Services.addApplicationService(builder.Configuration);

// and here is the part of the identity for including the token and all it's functionality 
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200","https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
