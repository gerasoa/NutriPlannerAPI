using CCRS.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCRS.Data.Mappings
{
    public class DoctorMapping : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Specialty)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.OffersOnlineConsultations)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : N => Doctor : Patients
            builder.HasMany(f => f.Patients)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId);

            builder.ToTable("Doctor");
        }
    }
}
