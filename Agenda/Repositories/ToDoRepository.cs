using Agenda.Data;
using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AgendaDbContext _dbContext;

        public ToDoRepository(AgendaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<ToDo> CreateToDoAsync(ToDo toDo)
        {
            var newToDo = await _dbContext.ToDos.AddAsync(toDo);
            await _dbContext.SaveChangesAsync();

            return newToDo.Entity;
        }

        public async ValueTask<ToDo> DeleteToDoAsync(Guid id)
        {
            var storageToDo = await _dbContext.ToDos.FirstOrDefaultAsync(x => x.Id == id);
            var deletedToDo = _dbContext.ToDos.Remove(storageToDo);
            await _dbContext.SaveChangesAsync();

            return deletedToDo.Entity;
        }
    }
}
