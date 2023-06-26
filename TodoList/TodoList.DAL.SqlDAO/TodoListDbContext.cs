using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TodoList.Entities;

namespace TodoList.DAL.SqlDAO
{
    public class TodoListDbContext : DbContext, ITodoListDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(item => item.Id);
                entity.HasIndex(item => item.Id).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public EntityEntry<TodoItem> Entry(TodoItem item) => base.Entry(item);
    }
}