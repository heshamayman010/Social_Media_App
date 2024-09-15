using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppDbContext(DbContextOptions options):
IdentityDbContext<AppUser,AppRole,int,IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>,
IdentityRoleClaim<int>,IdentityUserToken<int>>(options)
{
    public DbSet<LikeUser>Likes{set;get;}

    public DbSet<Messages> messages{set;get;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

// the relations between the appuser and the role 

            model.Entity<AppUser>().HasMany(x=>x.UserRoles).
            WithOne(u=>u.User).
            HasForeignKey(x=>x.UserId).IsRequired();

            // and for the role 
            model.Entity<AppRole>().HasMany(x=>x.UserRoles).
            WithOne(u=>u.Role).
            HasForeignKey(x=>x.RoleId).IsRequired();



                model.Entity<LikeUser>().
                HasKey(x=>new{x.SourceUserId,x.TargetUserId});

                model.Entity<LikeUser>().
                HasOne(x=>x.SourcUser).WithMany(x=>x.LikedUsers)
                .HasForeignKey(c=>c.SourceUserId).OnDelete(DeleteBehavior.Cascade);

              model.Entity<LikeUser>().
                HasOne(x=>x.TargetUser).WithMany(x=>x.LikedbyUsers)
                .HasForeignKey(c=>c.TargetUserId).OnDelete(DeleteBehavior.Cascade);

// and here is the configuration for the messages  and we will apply the soft delete 

    model.Entity<Messages>().HasOne(x=>x.Recipient)
    .WithMany(x=>x.MessagesReceived)
    .OnDelete(DeleteBehavior.Restrict);

    model.Entity<Messages>().HasOne(x=>x.Sender)
    .WithMany(x=>x.MessagesSent)
    .OnDelete(DeleteBehavior.Restrict);
    }





}
