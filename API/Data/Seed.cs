using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data;

public class Seed
{


public static async Task SeedUsers(UserManager<AppUser>userManager,RoleManager<AppRole>roleManager){

if(await userManager.Users.AnyAsync())return ;

var usersdata= await File.ReadAllTextAsync("Data/UserSeedData.json");

// now we will serialize  the user data 

var optionsforserialize=new JsonSerializerOptions{PropertyNameCaseInsensitive=true};

// now the users 

var users=JsonSerializer.Deserialize<List<AppUser>>(usersdata,optionsforserialize);

// and now to add the password hash and salt 

if(users==null)return;

var roles=new List<AppRole>{
new AppRole{Name="Member"},
new AppRole{Name="Admin"},
new AppRole{Name="Moderator"},


};

foreach(var role in roles){

    await roleManager.CreateAsync(role);
}

foreach(var user in users){
    user.UserName=user.UserName!.ToLower();
await userManager.CreateAsync(user,"Pa$$w0rd");
// here we will make all the users as members 
await userManager.AddToRoleAsync(user,"Member");
}


// and now to create the admins 
var admin=new AppUser{
UserName="admin",
KnownAs="admin",
Gender="",
City="",
Country="",
};

await userManager.CreateAsync(admin,"Pa$$w0rd");
await userManager.AddToRolesAsync(admin,["Admin","Moderator"]);

}
}
