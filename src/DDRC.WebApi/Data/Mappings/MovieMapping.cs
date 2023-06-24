using DDRC.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDRC.WebApi.Data.Mappings
{
    public class MovieMapping : IEntityTypeConfiguration<MovieModel>
    {
        public void Configure(EntityTypeBuilder<MovieModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();
        }
    }
}
