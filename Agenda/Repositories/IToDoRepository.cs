using Agenda.Models;

namespace Agenda.Repositories
{
    public interface IToDoRepository
    {
        ValueTask<ToDo> CreateToDoAsync(ToDo toDo);
        ValueTask<ToDo> DeleteToDoAsync(Guid id);
    }
}
