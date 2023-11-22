using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agenda.Services
{
    public partial class UpdateHandlerService : IUpdateHandler
    {
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var updateHandler = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(botClient,update,cancellationToken),
                _ => HandleUnknownUpdateAsync(botClient, update,cancellationToken),
            };

            try
            {
                await updateHandler;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
