using Reminder.Domain.Entities.Enums;

namespace Reminder.Domain.Entities;

public record Reminder(string Text, DateTime ReminderIn, ReminderType ReminderType = ReminderType.OncePerYear)
{
    public bool ShouldNotify(DateTime currentTime)
    {
        return ReminderType switch
               {
                   ReminderType.OnceTime => currentTime.Date == ReminderIn.Date,
                   ReminderType.OncePerYear => currentTime.Day == ReminderIn.Day &&
                                           currentTime.Month == ReminderIn.Month,
                   ReminderType.OncePerMonth => currentTime.Day == ReminderIn.Day,
                   _ => false,
               };
    }
}