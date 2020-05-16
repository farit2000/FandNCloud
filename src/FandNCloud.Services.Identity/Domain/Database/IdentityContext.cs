using System;
using FandNCloud.Services.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FandNCloud.Services.Identity.Domain.Database
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<InvalidToken> InvalidTokens {get; set;}
        
        public IdentityContext()
        {
        }
        
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity => 
                entity.Property(e => e.Email)
                    .IsRequired()
            );

            builder.Entity<FandNCloud.Services.Identity.Domain.Models.RefreshToken>()
                .HasOne(e => e.User)
                .WithMany(e => e.RefreshTokens)
                .HasForeignKey(e => e.UserId);
            
            builder.Entity<FandNCloud.Services.Identity.Domain.Models.InvalidToken>()
                .HasOne(e => e.User)
                .WithMany(e => e.InvalidTokens)
                .HasForeignKey(e => e.UserId);
        }
    }
}