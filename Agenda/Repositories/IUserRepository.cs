using Agenda.Models;
using Telegram.Bot.Types;

namespace Agenda.Repositories
{
    public interface IChallengerRepository
    {
        ValueTask<Challenger> CreateChallengerAsync(Challenger challenger);
        ValueTask<Challenger> DeleteChallengerAsync(long id);
    }
}
