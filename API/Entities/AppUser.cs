using System;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppUser:IdentityUser<int>
{


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



// here is the part of the like functionality 
public List<LikeUser>LikedbyUsers { get; set; }=[];
 public List<LikeUser>LikedUsers { get; set; }=[];


// for the part of the messages 

public List<Messages> MessagesSent{set;get;}=[];

public List<Messages> MessagesReceived{set;get;}=[];


// and for the .net identity

public ICollection<AppUserRole> UserRoles{set;get;}=[];



public int GetAge(){

// using the extension method we did 
  return  DateOfBirth.CalculateAge();
}
}
