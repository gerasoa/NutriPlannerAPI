using CCRS.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CCRS.Data.Mappings
{
    public class ConsultationConfigMapping : IEntityTypeConfiguration<ConsultationConfig>
    {
        public void Configure(EntityTypeBuilder<ConsultationConfig> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ConsultationType)
                .IsRequired();

            builder.Property(c => c.Duration)
                .IsRequired();

            builder.Property(c => c.TimeBetweenConsults)
                .IsRequired();

            builder.Property(c => c.DoctorId)
                .IsRequired();

            builder.Property(c => c.LunchBreakStart)
                .IsRequired();

            builder.Property(c => c.LunchBreakEnd)
                .IsRequired();

            builder.HasMany(c => c.AvailableSlots)
           .WithOne(a => a.ConsultationConfig)
           .HasForeignKey(a => a.ConsultationConfigId);

            builder.HasMany(c => c.OfficeLocations)
                .WithOne(o => o.ConsultationConfig)
                .HasForeignKey(o => o.ConsultationConfigId);

            builder.ToTable("ConsultationConfigs");
        }
    }
}
