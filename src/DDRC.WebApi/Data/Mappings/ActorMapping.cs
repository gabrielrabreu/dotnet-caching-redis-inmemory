using DDRC.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDRC.WebApi.Data.Mappings
{
    public class ActorMapping : IEntityTypeConfiguration<ActorModel>
    {
        public void Configure(EntityTypeBuilder<ActorModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
