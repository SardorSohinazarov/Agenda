using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Agenda.Services.BackGroundServices
{
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
            await _botClient.GetMeAsync(stoppingToken);

            Console.WriteLine("Botimiz eshitishni boshladi");

            _botClient.StartReceiving(
                _updateHandler.HandleUpdateAsync,
                _updateHandler.HandlePollingErrorAsync,
                new ReceiverOptions() { ThrowPendingUpdates = true },
                stoppingToken);
        }
    }
}
