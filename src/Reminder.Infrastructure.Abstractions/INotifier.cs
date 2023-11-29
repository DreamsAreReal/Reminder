namespace Reminder.Infrastructure.Abstractions;

public interface INotifier
{
    public Task NotifyAsync(Domain.Entities.Reminder reminder, CancellationToken token);
}