using System;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppRole:IdentityRole<int>
{

public ICollection<AppUserRole> UserRoles{set;get;}=[];
}