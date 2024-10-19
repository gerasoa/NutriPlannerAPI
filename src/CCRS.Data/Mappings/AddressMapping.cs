using CCRS.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCRS.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.HouseNumber)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.PostalCode)
                .IsRequired()
                .HasColumnType("varchar(10)");


            builder.ToTable("Address");
        }
    }
}
