namespace Agenda.Models
{
    public class Challenger
    {
        public long Id { get; set; }
        public long TelegramId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public long ChatId { get; set; }

        public List<ToDo>? ToDoList { get; set; }
    }
}
