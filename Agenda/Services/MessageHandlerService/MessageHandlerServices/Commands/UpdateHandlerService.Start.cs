using Agenda.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Agenda.Services;

public partial class UpdateHandlerService
{
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
            ChatId = update.Message.Chat.Id
        };

        if (tgUser.LastName != null)
            user.LastName = tgUser.LastName;

        if (tgUser.Username != null)
            user.Username = tgUser.Username;

        var savedUser = await _challengerRepository.CreateChallengerAsync(user);

        await SendTextMessageAsync($"{savedUser.FirstName} Ro'yhatga olindi", botClient, update, cancellationToken);
    }
}
