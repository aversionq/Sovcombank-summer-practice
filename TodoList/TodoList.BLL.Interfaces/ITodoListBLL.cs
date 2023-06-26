using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.DTOs;

namespace TodoList.BLL.Interfaces
{
    public interface ITodoListBLL
    {
        public Task<TodoItemDTO> GetTodoItemById(Guid id);
        public Task<List<TodoItemDTO>> GetAllTodoItems();
        public Task AddTodoItem(TodoItemDTO item);
        public Task DeleteTodoItem(Guid id);
        public Task SetTodoItemAsDone(Guid id);
        public Task<List<TodoItemDTO>> SearchTodoItems(string nameSubstring);
        public Task<List<TodoItemDTO>> SortTodoItemsByPriority(bool isAsc);
    }
}
