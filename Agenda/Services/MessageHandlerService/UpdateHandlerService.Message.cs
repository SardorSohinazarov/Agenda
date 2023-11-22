using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agenda.Services;

public partial class UpdateHandlerService
{
    private async Task HandleMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        var messageHandler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botClient, update, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, update, cancellationToken),
        };

        try
        {
            await messageHandler;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private ValueTask HandleUnknownMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
