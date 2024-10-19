namespace CCRS.Business.Models
{
    public enum ScheduleStatus
    {
        Pending,      // The appointment is scheduled but not yet confirmed.
        Confirmed,    // The appointment has been confirmed.
        Completed,    // The appointment has taken place.
        Cancelled,    // The appointment has been cancelled.
        Rescheduled    // The appointment has been rescheduled to a different time.
    }
}