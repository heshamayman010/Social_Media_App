using System;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppUserRole:IdentityUserRole<int>
// this class is join table between the user table adn the role table 
{

public AppUser User { get; set; }=null!;

public AppRole Role{set;get;}=null!;

}
