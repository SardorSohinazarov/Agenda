using Agenda.Models;

namespace Agenda.Repositories
{
    public interface IChallengerRepository
    {
        ValueTask<Challenger> CreateChallengerAsync(Challenger challenger);
        ValueTask<Challenger> DeleteChallengerAsync(long id);
        ValueTask<Challenger> GetChallengerFromByIdAsync(long id);
        ValueTask<List<Challenger>> GetChallengesAsync();
    }
}
