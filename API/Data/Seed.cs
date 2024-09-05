using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data;

public class Seed
{


public static async Task SeedUsers(AppDbContext context){

if(await context.appUsers.AnyAsync())return ;

var usersdata= await File.ReadAllTextAsync("Data/UserSeedData.json");

// now we will serialize  the user data 

var optionsforserialize=new JsonSerializerOptions{PropertyNameCaseInsensitive=true};

// now the users 

var users=JsonSerializer.Deserialize<List<AppUser>>(usersdata,optionsforserialize);

// and now to add the password hash and salt 

if(users==null)return;


foreach(var user in users){

using var hmac=new HMACSHA512();

user.UserName=user.UserName.ToLower();

user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
user.PasswordSalt=hmac.Key;
context.appUsers.Add(user);
}

await context.SaveChangesAsync();


}



}
