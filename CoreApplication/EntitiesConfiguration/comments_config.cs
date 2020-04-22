using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class comments_config : IEntityTypeConfiguration<comments>
    {
        public void Configure(EntityTypeBuilder<comments> builder)
        {
            
            builder.HasKey(x=>x.id);
            
 
            builder.HasOne<website>(x=>x.website)
                   .WithMany(x=>x.comments)
                   .HasForeignKey(x=>x.webid)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(x=>x.user)
                   .WithMany(x=>x.comments)
                   .HasForeignKey(x=>x.userid)
                   .OnDelete(DeleteBehavior.Restrict);       

        }
    }
}