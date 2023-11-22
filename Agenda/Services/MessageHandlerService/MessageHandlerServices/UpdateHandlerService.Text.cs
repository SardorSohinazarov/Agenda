using Telegram.Bot.Types;
using Telegram.Bot;

namespace Agenda.Services
{
    public partial class UpdateHandlerService
    {
        private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var text = update.Message.Text;

            var textHandler = text switch
            {
                "/start" => StartCommandAsync(botClient, update, cancellationToken),
                "/todo" => ToDoCommandAsync(botClient, update, cancellationToken),
                "/done" => DoneCommandAsync(botClient, update, cancellationToken),
                _ => UnknownCommandAsync(botClient, update, cancellationToken)
            };
        }

        private async Task UnknownCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task DoneCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task ToDoCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task StartCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
