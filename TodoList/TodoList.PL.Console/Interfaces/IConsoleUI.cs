using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.DTOs;

namespace TodoList.PL.Console.Interfaces
{
    public interface IConsoleUI
    {
        //public Task Run();
        //public void PrintAllTodoItems(List<TodoItemDTO> todoItems);
        //public void PrintTodoItem(TodoItemDTO item);
        //public TodoItemDTO InputTodoItem();
        //public int InputTodoItemIndex(int minIndex, int maxIndex);
        //public string InputTodoItemNameSubstring();
        public Task GetAllTodoItems();
        public Task AddTodoItem();
        public Task DeleteTodoItem();
        public Task SetTodoItemAsDone();
        public Task SearchTodoItems();
        public Task SortTodoItemsByPriority(bool isAsc);
    }
}
