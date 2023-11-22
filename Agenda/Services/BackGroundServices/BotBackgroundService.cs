using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Agenda.Services.BackGroundServices;

public class BotBackgroundService : BackgroundService
{
    private TelegramBotClient _botClient;
    private IUpdateHandler _updateHandler;

    public BotBackgroundService(
        TelegramBotClient botClient,
        IUpdateHandler updateHandler)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bot = await _botClient.GetMeAsync(stoppingToken);

        Console.WriteLine($"Botimiz eshitishni boshladi:{bot.Username}");

        _botClient.StartReceiving(
            updateHandler: _updateHandler.HandleUpdateAsync,
            pollingErrorHandler: _updateHandler.HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions() { ThrowPendingUpdates = true },
            cancellationToken: stoppingToken);
    }
}
