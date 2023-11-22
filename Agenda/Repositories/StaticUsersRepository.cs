using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repositories;

public class StaticUsersRepository
{
    public static async ValueTask<List<Challenger>> GetAllChallengesAsync()
    {
        AgendaDbContext agendaDbContext = new AgendaDbContext(new DbContextOptions<AgendaDbContext>());

        var users = agendaDbContext.Challengers.Include(x => x.ToDoList).ToList();

        return users;
    }
}
