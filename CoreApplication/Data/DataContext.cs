using EzilineApp.Api.CoreApplication.EntitiesConfiguration;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EzilineApp.Api.CoreApplication.Data
{
    public class DataContext:IdentityDbContext<User, Role,int,
    IdentityUserClaim<int>,
    UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        
        public DbSet<website>   websites {get;set;}
        public DbSet<comments>  comments {get;set;}
        public DbSet<profile>   profile  {get;set;}
        public DbSet<ratings>   ratings  {get;set;}
        public DbSet<summary>   summary  {get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
                 base.OnModelCreating(builder);

                 builder.ApplyConfiguration(new Identity_userconfig());
                 
                 builder.ApplyConfiguration(new user_config());

                 builder.ApplyConfiguration(new website_config());

                 builder.ApplyConfiguration(new comments_config());
                 
                 builder.ApplyConfiguration(new profile_config());

                 builder.ApplyConfiguration(new ratings_config());

                 builder.ApplyConfiguration(new average_config());

        }
    }


}