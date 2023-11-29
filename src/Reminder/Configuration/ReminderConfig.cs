namespace Reminder.Configuration;

public class ReminderConfig
{
    public required IEnumerable<Domain.Entities.Reminder> Reminders { get; init; }
    public required string TimezoneName { get; init; }

    public TimeZoneInfo GetTimeZone()
    {
        return TimeZoneInfo.FindSystemTimeZoneById(TimezoneName);
    }
}