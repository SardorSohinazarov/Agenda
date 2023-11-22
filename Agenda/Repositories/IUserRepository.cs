using Agenda.Models;
using Telegram.Bot.Types;

namespace Agenda.Repositories
{
    public interface IUserRepository
    {
        ValueTask<Challenger> CreateUserAsync(Challenger challenger);
    }
}
