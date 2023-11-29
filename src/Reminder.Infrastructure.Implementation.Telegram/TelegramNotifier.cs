using Microsoft.Extensions.Options;
using Reminder.Infrastructure.Abstractions;
using Reminder.Infrastructure.Implementation.Telegram.Configuration;
using Telegram.Bot;

namespace Reminder.Infrastructure.Implementation.Telegram;

public class TelegramNotifier(IOptions<TelegramBotConfig> telegramConfig) : INotifier
{
    private readonly ITelegramBotClient
        _telegramBotClient = new TelegramBotClient(telegramConfig.Value.Token);
    

    public async Task NotifyAsync(Domain.Entities.Reminder reminder, CancellationToken token)
    {
        await _telegramBotClient.SendTextMessageAsync(
            telegramConfig.Value.ChatId, reminder.Text, cancellationToken: token
        );
    }
}