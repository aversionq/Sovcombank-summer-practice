using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Entities;

namespace TodoList.DAL.SqlDAO
{
    public interface ITodoListDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync();
        public EntityEntry<TodoItem> Entry(TodoItem item);
    }
}
