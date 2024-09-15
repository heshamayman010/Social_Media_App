using System;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected AppDbContext()
    {
    }

    public DbSet<AppUser>appUsers{get;set;}
    public DbSet<LikeUser>Likes{set;get;}

    public DbSet<Messages> messages{set;get;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);
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
