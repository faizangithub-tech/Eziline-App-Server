using EzilineApp.Api.CoreApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzilineApp.Api.CoreApplication.EntitiesConfiguration
{
    public class profile_config : IEntityTypeConfiguration<profile>
    {
        public void Configure(EntityTypeBuilder<profile> builder)
        {

              builder.HasKey(x=>x.id);

              builder.HasOne<User>(x=>x.user)
                     .WithOne(x=>x.profile)
                     .HasForeignKey<profile>(p=>p.usrid);
                                              
        }
    }
}