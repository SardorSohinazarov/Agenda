using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repositories;

public class ChallengerRepository : IChallengerRepository
{
    private readonly AgendaDbContext _context;
    public ChallengerRepository(AgendaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(AgendaDbContext));
    }

    public async ValueTask<Challenger> CreateChallengerAsync(Challenger challenger)
    {
        var newChallenger = await _context.Challengers.AddAsync(challenger);
        await _context.SaveChangesAsync();

        return newChallenger.Entity;
    }

    public async ValueTask<Challenger> DeleteChallengerAsync(long id)
    {
        var storageChallenger = await _context.Challengers.FirstOrDefaultAsync(ch => ch.TelegramId == id);

        if (storageChallenger == null)
        {
            await Console.Out.WriteLineAsync("Bu yo'q");
        }

        var deletedChallenger = _context.Challengers.Remove(storageChallenger);
        await _context.SaveChangesAsync();

        return deletedChallenger.Entity;
    }

    public async ValueTask<Challenger> GetChallengerFromByIdAsync(long id)
    {
        var users = _context.Challengers.ToList();
        var user = await _context.Challengers.FirstOrDefaultAsync(ch => ch.TelegramId == id);

        if (user == null)
            Console.WriteLine();

        return user;
    }

    public async ValueTask<List<Challenger>> GetChallengesAsync()
    {
        var users = _context.Challengers.Include(x => x.ToDoList).ToList();

        return users;
    }
}
