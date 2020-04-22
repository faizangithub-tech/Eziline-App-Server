using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class ratings_config : IEntityTypeConfiguration<ratings>
    {
        public void Configure(EntityTypeBuilder<ratings> builder)
        {
            
                  builder.HasKey(x=>x.id);
                        
                  builder.HasOne<website>(x=>x.website)
                         .WithMany(x=>x.ratings)
                         .HasForeignKey(x=>x.webid);
                  
        }
    }
}