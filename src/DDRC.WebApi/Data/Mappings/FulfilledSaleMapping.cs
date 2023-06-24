using DDRC.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDRC.WebApi.Data.Mappings
{
    public class FulfilledSaleMapping : IEntityTypeConfiguration<FulfilledSaleModel>
    {
        public void Configure(EntityTypeBuilder<FulfilledSaleModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();
        }
    }
}
