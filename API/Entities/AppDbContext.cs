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


    }





}
