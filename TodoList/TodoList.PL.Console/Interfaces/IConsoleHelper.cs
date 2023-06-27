using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.DTOs;
using TodoList.BLL.Interfaces;

namespace TodoList.PL.Console.Interfaces
{
    public interface IConsoleHelper
    {
        public Task<TodoItemDTO> GetTodoItemById(Guid id);
        public Task<List<TodoItemDTO>> GetAllTodoItems();
        public Task AddTodoItem(TodoItemToAddDTO item);
        public Task DeleteTodoItem(Guid id);
        public Task SetTodoItemAsDone(Guid id);
        public Task<List<TodoItemDTO>> SearchTodoItems(string nameSubstring);
        public Task<List<TodoItemDTO>> SortTodoItemsByPriority(bool isAsc);
    }
}
