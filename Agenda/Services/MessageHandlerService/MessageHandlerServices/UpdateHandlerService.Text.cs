using Telegram.Bot.Types;
using Telegram.Bot;
using Agenda.Models;

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
                "/archive" => ArchiveCommandAsync(botClient, update, cancellationToken),
                _ => UnknownCommandAsync(botClient, update, cancellationToken)
            };
        }

        private async Task UnknownCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Unknown Command", botClient, update, cancellationToken);
        }

        private async Task DoneCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Done", botClient, update, cancellationToken);
        }

        private async Task ToDoCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("ToDo", botClient, update, cancellationToken);
        }

        private async Task StartCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var tgUser = update.Message.From;

            var existUser = await _repository.GetChallengerFromByIdAsync(tgUser.Id);

            if (existUser != null)
            {
                await SendTextMessageAsync($"{existUser.FirstName} Allaqachon ro'yhatga olingan", botClient, update, cancellationToken);
                return;
            }

            var user = new Challenger
            {
                TelegramId = tgUser.Id,
                FirstName = tgUser.FirstName,
            };

            if(tgUser.LastName != null)
                user.LastName = tgUser.LastName;
            if(tgUser.Username != null)
                user.Username = tgUser.Username;

            var savedUser = await _repository.CreateChallengerAsync(user);

            await SendTextMessageAsync($"{savedUser.FirstName} Ro'yhatga olindi", botClient, update, cancellationToken);
        }
        
        private async Task ArchiveCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Archive", botClient, update, cancellationToken);
        }

        private async Task SendTextMessageAsync(string text,ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: text,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                replyToMessageId: update.Message.MessageId,
                cancellationToken: cancellationToken
                );
        }
    }
}
