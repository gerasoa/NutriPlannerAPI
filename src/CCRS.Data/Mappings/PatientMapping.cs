using CCRS.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCRS.Data.Mappings
{
    public class PatientMapping : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Gender)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.DoB)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.HasOne(f => f.Address)
                .WithOne(e => e.Patient);



            builder.ToTable("Patients");
        }
    }
}
