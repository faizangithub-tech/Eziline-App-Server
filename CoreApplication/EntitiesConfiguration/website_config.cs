using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class website_config : IEntityTypeConfiguration<website>
    {
        public void Configure(EntityTypeBuilder<website> builder)
        {
            
               builder.HasKey(x=>x.id);

               builder.HasOne<User>(x=>x.user)
                      .WithMany(x=>x.websites)
                      .HasForeignKey(x=>x.userid);       
        }
    }
}