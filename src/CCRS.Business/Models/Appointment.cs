using CCRS.Business.Models.Enums;
using System.Text.Json.Serialization;

namespace CCRS.Business.Models
{
    public class Appointment : Entity
    {
        /// <summary>
        /// The time at which the appointment is scheduled.
        /// </summary>
        public DateTime ScheduledTime { get; set; }

        /// <summary>
        /// The current status of the appointment, indicating whether it is pending, confirmed, completed, cancelled, or rescheduled.
        /// </summary>
        public ScheduleStatus Status { get; set; }

        /// <summary>
        /// The location of the appointment.
        /// </summary>
        //public AppointmentLocation AppointmentLocation { get; set; }

        /// <summary>
        /// The unique identifier of the patient profile associated with this appointment.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// The unique identifier of the patient profile associated with this appointment.
        /// </summary>
        public Guid DoctorId { get; set; }
        /// <summary>
        /// Any additional comments or notes related to the appointment, providing further context or information.
        /// </summary>
        public string Comments { get; set; }


        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }
    }
}