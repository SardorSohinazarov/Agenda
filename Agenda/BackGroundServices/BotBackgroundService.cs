using Agenda.Services;
using Telegram.Bot;

namespace Agenda.BackGroundServices
{
    public class BotBackgroundService : BackgroundService
    {
        private TelegramBotClient _botClient;

        public BotBackgroundService(TelegramBotClient botClient)
            => _botClient = botClient;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Botimiz eshitishni boshladi");

            _botClient.StartReceiving<UpdateHandlerService>(null,stoppingToken);

            return Task.CompletedTask;
        }
    }
}
