namespace CCRS.Business.Models.Enums
{
    public enum ScheduleStatus
    {
        Pending = 1,      // The appointment is scheduled but not yet confirmed.
        Confirmed = 2,    // The appointment has been confirmed.
        Completed = 3,    // The appointment has taken place.
        Cancelled = 4,    // The appointment has been cancelled.
        Rescheduled = 5    // The appointment has been rescheduled to a different time.
    }
}