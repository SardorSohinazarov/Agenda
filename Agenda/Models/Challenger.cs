using Newtonsoft.Json;

namespace Agenda.Models
{
    public class Challenger
    {
        public long Id { get; set; }
        public long TelegramId { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? Username { get; set; }

        public List<ToDo>? ToDoList { get; set; }
    }
}
