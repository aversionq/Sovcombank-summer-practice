using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DAL.Interfaces;
using TodoList.Entities;

namespace TodoList.DAL.SqlDAO
{
    public class SqlDAO : ITodoListDAO
    {
        private ITodoListDbContext _dbContext;

        public SqlDAO(ITodoListDbContext ctx)
        {
            _dbContext = ctx;
        }

        public async Task AddTodoItem(TodoItem item)
        {
            _dbContext.TodoItems.Add(item);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(item).State = EntityState.Detached;
        }

        public async Task DeleteTodoItem(Guid id)
        {
            var item = new TodoItem
            {
                Id = id
            };
            _dbContext.TodoItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(item).State = EntityState.Deleted;
        }

        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            return await _dbContext.TodoItems
                .OrderBy(x => x.IsDone)
                /* Проблемы с трэкингом в консольном приложении:
                   Отслеживается несколько раз одна сущность (с одним
                   и тем же Id). */
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemById(Guid id)
        {
            var item = _dbContext.TodoItems.Where(x => x.Id == id);
            return await item.FirstOrDefaultAsync();
        }

        public async Task<List<TodoItem>> SearchTodoItems(string nameSubstring)
        {
            var items = _dbContext.TodoItems
                .Where(x => x.Name.ToLower().Contains(nameSubstring.ToLower()));
            return await items.ToListAsync();
        }

        public async Task SetTodoItemAsDone(Guid id)
        {
            var item = new TodoItem
            {
                Id = id,
                IsDone = true
            };
            _dbContext.TodoItems.Attach(item);
            _dbContext.Entry(item).Property(x => x.IsDone).IsModified = true;

            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(item).State = EntityState.Detached;
        }
    }
}
