using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Entities;

namespace TodoList.DAL.Interfaces
{
    public interface ITodoListDAO
    {
        public Task<TodoItem> GetTodoItemById(Guid id);
        public Task<List<TodoItem>> GetAllTodoItems();
        public Task AddTodoItem(TodoItem item);
        public void DeleteTodoItem(Guid id);
        public Task SetTodoItemAsDone(Guid id);
        public Task<List<TodoItem>> SearchTodoItems(string nameSubstring);
    }
}
