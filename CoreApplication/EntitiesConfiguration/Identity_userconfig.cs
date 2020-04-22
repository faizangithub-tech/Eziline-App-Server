using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class Identity_userconfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            
            builder.HasKey(ur=>new{ur.RoleId,ur.UserId});
            builder.HasOne(x=>x.Role)
                   .WithMany(y=>y.UserRoles)
                   .HasForeignKey(x=>x.RoleId)
                   .IsRequired();

            builder.HasOne(x=>x.User)
                   .WithMany(y=>y.UserRoles)
                   .HasForeignKey(x=>x.UserId)
                   .IsRequired();
                   
                
                
        }
    }
}