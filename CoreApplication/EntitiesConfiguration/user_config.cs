using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class user_config : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
             
             builder.HasIndex(x=>x.Email)
                    .IsUnique();

             builder.Property(x=>x.Email)
                    .IsRequired();


        }
    }
}