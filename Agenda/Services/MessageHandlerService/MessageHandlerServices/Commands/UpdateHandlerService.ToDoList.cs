using Telegram.Bot;
using Telegram.Bot.Types;

namespace Agenda.Services;

public partial class UpdateHandlerService
{
    private async ValueTask ToDoListCommandAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var todoList = await _toDoRepository.GetToDoListFromUserIdAsync(update.Message.From.Id);

        if (todoList == null)
        {
            await SendTextMessageAsync("Hali bironta ham todo yo'q", botClient, update, cancellationToken);
        }
        else
        {
            var todoListString = string.Empty;

            for (var i = 0; i < todoList.Count; i++)
            {
                todoListString = todoListString + $"{i + 1} {todoList[i].Description} \n";
            }

            await SendTextMessageAsync(todoListString, botClient, update, cancellationToken);
        }
    }
}
