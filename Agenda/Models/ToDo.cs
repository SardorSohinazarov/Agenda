namespace Agenda.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public long ChallengerId { get; set; }
        public Challenger? Challenger { get; set; }
    }
}
