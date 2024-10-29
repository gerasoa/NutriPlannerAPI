using CCRS.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCRS.Data.Mappings
{
    public class AppointmentMapping : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Comments)
                .IsRequired()
                .HasColumnType("varchar(500)");
                      
            builder.HasOne(f => f.Patient)
                .WithOne(e => e.Appointment);

            builder.HasOne(f => f.Doctor)
                .WithOne(e => e.Appointment);

        builder.ToTable("Appointment");
        }
    }
}
