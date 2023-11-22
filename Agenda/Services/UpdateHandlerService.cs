using Agenda.Repositories;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Agenda.Services
{
    public partial class UpdateHandlerService : IUpdateHandler
    {
        private IChallengerRepository _challengerRepository;
        private IToDoRepository _toDoRepository;
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdateHandlerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateAsyncScope())
            {
                _toDoRepository = scope.ServiceProvider.GetService<IToDoRepository>();
                _challengerRepository = scope.ServiceProvider.GetService<IChallengerRepository>();
            }

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

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
