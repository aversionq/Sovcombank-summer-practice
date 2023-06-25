using TodoList.Entities;

namespace TodoList.BLL.DTOs
{
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Priority { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; } = false;
    }
}