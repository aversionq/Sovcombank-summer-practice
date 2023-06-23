namespace TodoList.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public PriorityType Priority { get; set; }
        public string Text { get; set; } = null!;
    }
}