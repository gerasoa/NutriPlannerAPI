using CCRS.Business.Models.Enums;

namespace CCRS.Api.ViewModels
{
    public class AppointmentViewModel
    {
        public DateTime ScheduledTime { get; set; }

        public ScheduleStatus Status { get; set; }

        public Guid PatientId { get; set; }

        public Guid DoctorId { get; set; }

        public string Comments { get; set; }
    }
}