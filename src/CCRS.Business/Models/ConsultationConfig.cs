using CCRS.Business.Models.Enums;

namespace CCRS.Business.Models
{
    public class ConsultationConfig : Entity
    {
        public ConsultationType ConsultationType { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan TimeBetweenConsults { get; set; }
        public Guid DoctorId { get; set; }
        public TimeSpan LunchBreakStart { get; set; }
        public TimeSpan LunchBreakEnd { get; set; }
        public List<AvailableSlot> AvailableSlots { get; set; }
        public List<OfficeLocation> OfficeLocations { get; set; }
        public ConsultationConfig()
        {
            AvailableSlots = new List<AvailableSlot>();
            OfficeLocations = new List<OfficeLocation>();
        }
    }

    public class AvailableSlot
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Slot { get; set; }
        public Guid ConsultationConfigId { get; set; }
        public ConsultationConfig ConsultationConfig { get; set; }
    }

    public class OfficeLocation
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string Location { get; set; }
        public Guid ConsultationConfigId { get; set; }
        public ConsultationConfig ConsultationConfig { get; set; }
    }
}
