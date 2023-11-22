using Agenda.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Agenda.Services;

public partial class UpdateHandlerService
{
    private async ValueTask HandleTextMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var text = update.Message.Text;
        var command = string.Empty;

        if (text.StartsWith("/start"))
        {
            command = "/start";
        }
        else if (text.StartsWith("/todo"))
        {
            command = "/todo";
        }
        else if (text.StartsWith("/done"))
        {
            command = "/done";
        }
        else if (text.StartsWith("/archive"))
        {
            command = "/archive";
        }
        else if (text.StartsWith("/listtodo"))
        {
            command = "/listtodo";
        }

        var textHandler = command switch
        {
            "/start" => StartCommandAsync(botClient, update, cancellationToken),
            "/todo" => ToDoCommandAsync(botClient, update, cancellationToken),
            "/done" => DoneCommandAsync(botClient, update, cancellationToken),
            "/archive" => ArchiveCommandAsync(botClient, update, cancellationToken),
            "/listtodo" => ToDoListCommandAsync(botClient, update, cancellationToken),
            _ => UnknownCommandAsync(botClient, update, cancellationToken)
        };

        try
        {
            await textHandler;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hammasi yaxshi");
        }
    }

    private async ValueTask UnknownCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        => await SendTextMessageAsync("Unknown Command", botClient, update, cancellationToken);

    private async ValueTask DoneCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        => await SendTextMessageAsync("Done", botClient, update, cancellationToken);

    private async ValueTask ArchiveCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        => await SendTextMessageAsync("Archive", botClient, update, cancellationToken);

    private async ValueTask SendTextMessageAsync(string text, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
