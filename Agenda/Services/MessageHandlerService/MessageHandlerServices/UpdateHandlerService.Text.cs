using Telegram.Bot.Types;
using Telegram.Bot;
using Agenda.Models;

namespace Agenda.Services
{
    public partial class UpdateHandlerService
    {
        private async ValueTask HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var text = update.Message.Text;
            var command = string.Empty;
            if(text.StartsWith("/start"))
            {
                command = "/start";
            }
            else if(text.StartsWith("/todo"))
            {
                command = "/todo";
            }
            else if(text.StartsWith("/done"))
            {
                command = "/done";
            }
            else if(text.StartsWith("/archive"))
            {
                command = "/archive";
            }

            var textHandler = command switch
            {
                "/start" => StartCommandAsync(botClient, update, cancellationToken),
                "/todo" => ToDoCommandAsync(botClient, update, cancellationToken),
                "/done" => DoneCommandAsync(botClient, update, cancellationToken),
                "/archive" => ArchiveCommandAsync(botClient, update, cancellationToken),
                _ => UnknownCommandAsync(botClient, update, cancellationToken)
            };
        }

        private async ValueTask UnknownCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Unknown Command", botClient, update, cancellationToken);
        }

        private async ValueTask DoneCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Done", botClient, update, cancellationToken);
        }

        private async ValueTask ToDoCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var challangerId = await _challengerRepository.GetChallengerFromByIdAsync(update.Message.From.Id);
            
            var newTodo = new ToDo
            {
                Description = update.Message.Text,
                ChallengerId = challangerId.Id,
                CreatedDate = DateTime.Now,
            };

            await _toDoRepository.CreateToDoAsync(newTodo);

            await SendTextMessageAsync("ToDo listga qo'shildi", botClient, update, cancellationToken);
        }

        private async ValueTask StartCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var tgUser = update.Message.From;

            var existUser = await _challengerRepository.GetChallengerFromByIdAsync(tgUser.Id);

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

            var savedUser = await _challengerRepository.CreateChallengerAsync(user);

            await SendTextMessageAsync($"{savedUser.FirstName} Ro'yhatga olindi", botClient, update, cancellationToken);
        }
        
        private async ValueTask ArchiveCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await SendTextMessageAsync("Archive", botClient, update, cancellationToken);
        }

        private async ValueTask SendTextMessageAsync(string text,ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
