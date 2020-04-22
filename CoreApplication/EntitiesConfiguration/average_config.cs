using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class average_config:IEntityTypeConfiguration<summary>
    {
         public void Configure(EntityTypeBuilder<summary> builder)
         {
                
                 builder.HasKey(x=>x.id);

                 builder.HasOne<website>(x=>x.website)
                        .WithOne(x=>x.summary)
                        .HasForeignKey<summary>(x=>x.webid);
                        
         }

    }
}