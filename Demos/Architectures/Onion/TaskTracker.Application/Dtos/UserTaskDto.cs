namespace TaskTracker.Application.DTOs
{
    public class UserTaskDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = default!;
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
