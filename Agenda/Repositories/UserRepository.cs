using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repositories
{
    public class ChallengerRepository : IChallengerRepository
    {
        private readonly AgendaDbContext _context;

        public ChallengerRepository(AgendaDbContext context)
            => _context = context;

        public async ValueTask<Challenger> CreateChallengerAsync(Challenger challenger)
        {
            var newChallenger = await _context.Challengers.AddAsync(challenger);
            await _context.SaveChangesAsync();

            return newChallenger.Entity;
        }

        public async ValueTask<Challenger> DeleteChallengerAsync(long id)
        {
            var storageChallenger = await _context.Challengers.FirstOrDefaultAsync(ch => ch.Id == id);

            if(storageChallenger == null)
            {
                await Console.Out.WriteLineAsync("Bu yo'q");
            }

            var deletedChallenger = _context.Challengers.Remove(storageChallenger);

            return deletedChallenger.Entity;
        }
    }
}
