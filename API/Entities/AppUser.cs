using System;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppUser
{

    public int Id { get; set; }
public required string UserName { get; set; }
public required byte[] PasswordHash { get; set; }
// this password salt is used to add custom value to the passwords so that after hashing the same passwords will have different hashes
public required byte[] PasswordSalt { get; set; }



}
