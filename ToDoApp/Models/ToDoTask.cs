namespace ToDoApp.Models
{
    public class ToDoTask
    {
        public int Id { get; init; }
        public required string Title { get; set; } = string.Empty;
        public List<string> Objectives { get; set; } = new();
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;

        public string DisplayToDoTaskDetails()
        {
            var status = IsCompleted ? "✓ Completed" : "○ Pending";
            var objectives = Objectives.Count > 0 
                ? string.Join("\n  - ", Objectives) 
                : "No objectives";
            
            return $"[{Id}] {Title} ({status})\n" +
                $"Created: {CreatedAt:yyyy-MM-dd HH:mm}\n" +
                $"Objectives:\n  - {objectives}";
        }
    }
}