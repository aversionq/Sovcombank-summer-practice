using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.DTOs;
using TodoList.BLL.Interfaces;
using TodoList.PL.Console.Interfaces;

namespace TodoList.PL.Console.Implementations
{
    public class ConsoleHelper : IConsoleHelper
    {
        private ITodoListBLL _bll;

        public ConsoleHelper(ITodoListBLL todoListBLL)
        {
            _bll = todoListBLL;
        }

        public async Task AddTodoItem(TodoItemDTO item)
        {
            try
            {
                await _bll.AddTodoItem(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTodoItem(Guid id)
        {
            try
            {
                await _bll.DeleteTodoItem(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TodoItemDTO>> GetAllTodoItems()
        {
            try
            {
                return await _bll.GetAllTodoItems();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TodoItemDTO> GetTodoItemById(Guid id)
        {
            try
            {
                return await _bll.GetTodoItemById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TodoItemDTO>> SearchTodoItems(string nameSubstring)
        {
            try
            {
                return await _bll.SearchTodoItems(nameSubstring);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SetTodoItemAsDone(Guid id)
        {
            try
            {
                await _bll.SetTodoItemAsDone(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TodoItemDTO>> SortTodoItemsByPriority(bool isAsc)
        {
            try
            {
                return await _bll.SortTodoItemsByPriority(isAsc);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
