using Agenda.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Agenda.Services;

public partial class UpdateHandlerService
{
    private async ValueTask ToDoCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var challanger = await _challengerRepository.GetChallengerFromByIdAsync(update.Message.From.Id);

        var newTodo = new ToDo
        {
            Description = update.Message.Text,
            ChallengerId = challanger.Id,
            CreatedDate = DateTime.Now,
        };

        await _toDoRepository.CreateToDoAsync(newTodo);

        await SendTextMessageAsync("ToDo listga qo'shildi", botClient, update, cancellationToken);
    }
}
