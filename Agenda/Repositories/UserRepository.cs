﻿using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repositories
{
    public class ChallengerRepository : IChallengerRepository
    {
        private readonly AgendaDbContext _context;
        private readonly ILogger<ChallengerRepository> _logger;

        public ChallengerRepository(AgendaDbContext context, ILogger<ChallengerRepository> logger)
        {
            _context = context;
            _logger = logger;
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

            if(storageChallenger == null)
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
            _logger.Log(LogLevel.Information,"Hammasi yaxshi xozircha");

            if(user == null)
                Console.WriteLine();

            return user;
        }
    }
}
