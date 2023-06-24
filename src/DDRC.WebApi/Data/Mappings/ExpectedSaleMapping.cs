using DDRC.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDRC.WebApi.Data.Mappings
{
    public class ExpectedSaleMapping : IEntityTypeConfiguration<ExpectedSaleModel>
    {
        public void Configure(EntityTypeBuilder<ExpectedSaleModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();
        }
    }
}
