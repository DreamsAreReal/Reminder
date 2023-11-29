using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Reminder.Configuration;
using Reminder.Infrastructure.Abstractions;
using Reminder.Infrastructure.Implementation.Telegram;
using Reminder.Infrastructure.Implementation.Telegram.Configuration;

Stopwatch timer = Stopwatch.StartNew();
IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                         .AddJsonFile("appsettings.json", true, true)
                                                         .AddUserSecrets<Program>()
                                                         .Build();

ServiceProvider serviceProvider = new ServiceCollection()
                                  .Configure<ReminderConfig>(configuration.GetSection(nameof(ReminderConfig)))
                                  .Configure<TelegramBotConfig>(
                                      configuration.GetSection(nameof(TelegramBotConfig))
                                  )
                                  .AddTransient<INotifier, TelegramNotifier>()
                                  .BuildServiceProvider();

INotifier? notifier = serviceProvider.GetService<INotifier>();
CancellationTokenSource cancellationTokenSource = new();
ReminderConfig? reminderConfig = serviceProvider.GetService<IOptions<ReminderConfig>>()!.Value;
DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, reminderConfig?.GetTimeZone() ?? TimeZoneInfo.Utc);

foreach (Reminder.Domain.Entities.Reminder reminder in reminderConfig?.Reminders ??
                                                       new List<Reminder.Domain.Entities.Reminder>())
{
    if (reminder.ShouldNotify(currentTime))
        await notifier?.NotifyAsync(reminder, cancellationTokenSource.Token)!;
}

timer.Stop();
Console.WriteLine("Elapsed time: {0}", timer.Elapsed);