using Agenda.Repositories;
using Telegram.Bot;

namespace Agenda.Services.BackGroundServices
{
    public class ReminderBackgroundService : BackgroundService
    {
        private TelegramBotClient _botClient;

        public ReminderBackgroundService(
            TelegramBotClient botClient
            )
        {
            _botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await Task.Delay(500);

            //sakkiz
            while(!stoppingToken.IsCancellationRequested) 
            {
                var now = DateTime.Now;
                if(
                    (now.Hour == 8 || now.Hour == 12 || now.Hour == 18) 
                    && now.Minute == 0 
                    && now.Second == 0)
                {
                    var users = await StaticUsersRepository.GetAllChallengesAsync();

                    foreach (var user in users)
                    {
                        string todoListString = string.Empty;
                        for(int i = 0;i< user.ToDoList.Count; i ++)
                        {
                            todoListString = todoListString + $"{i + 1} {user.ToDoList[i].Description} \n";
                        }

                        await SendTextToUsers(todoListString, user.ChatId, _botClient, stoppingToken);
                    }
                }
            }
        }

        private async ValueTask SendTextToUsers(string text,long chatId, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text,
                cancellationToken: cancellationToken);
        }
    }
}
