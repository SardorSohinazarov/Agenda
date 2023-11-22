using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
    public class AgendaDbContext:DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options):base(options) { }

        public DbSet<Challenger> Challengers { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Challenger>()
                .HasMany(x => x.ToDoList)
                .WithOne(x => x.Challenger)
                .HasForeignKey(x => x.ChallengerId);
        }
    }
}
