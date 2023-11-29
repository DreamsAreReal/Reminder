namespace Reminder.Infrastructure.Implementation.Telegram.Configuration;

public record TelegramBotConfig
{
    public required string ChatId { get; init; }
    public required string Token { get; init; }
}