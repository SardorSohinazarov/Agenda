using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
    public class AgendaDbContext:DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options):base(options) { }

        public DbSet<Challenger> Challengers { get; set; }
    }
}
