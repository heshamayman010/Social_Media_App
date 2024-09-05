using System;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppUser
{

    public int Id { get; set; }
public required string UserName { get; set; }
public  byte[] PasswordHash { get; set; }=[];
// this password salt is used to add custom value to the passwords so that after hashing the same passwords will have different hashes
public  byte[] PasswordSalt { get; set; }=[] ;

public DateOnly DateOfBirth { get; set; }

public required string  KnownAs { get; set; }
public DateTime Created{set;get;}=DateTime.UtcNow;
public DateTime LAstActiv {set;get;}=DateTime.UtcNow;

public required string Gender { get; set; }

public string? Introduction { get; set; }
public string? Interstes { get; set; }
public string? LookingFor { get; set; }

public required string City { get; set; }
public required string Country { get; set; }

public List<Photo> Photos{get;set;}=[];





public int GetAge(){

// using the extension method we did 
  return  DateOfBirth.CalculateAge();
}
}
