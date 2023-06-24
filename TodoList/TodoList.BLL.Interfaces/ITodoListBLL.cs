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
        public Task<IEnumerable<TodoItemDTO>> GetAllTodoItems();
        public Task AddTodoItem(TodoItemDTO item);
        public void DeleteTodoItem(Guid id);
        public Task SetTodoItemAsDone(Guid id);
        public Task<IEnumerable<TodoItemDTO>> SearchTodoItems(string nameSubstring);
        public Task<IEnumerable<TodoItemDTO>> SortTodoItemsByPriority(bool isAsc);
    }
}
