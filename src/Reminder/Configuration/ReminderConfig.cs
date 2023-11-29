namespace Reminder.Configuration;

public class ReminderConfig
{
    public required string TimezoneName { get; init; }
    public required IEnumerable<Domain.Entities.Reminder> Reminders { get; init; }

    public TimeZoneInfo GetTimeZone()
    {
        return TimeZoneInfo.FindSystemTimeZoneById(TimezoneName);
    }
}