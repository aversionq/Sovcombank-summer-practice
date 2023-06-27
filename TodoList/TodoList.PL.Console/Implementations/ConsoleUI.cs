using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.BLL.DTOs;
using TodoList.BLL.Interfaces;
using TodoList.PL.Console.Interfaces;

namespace TodoList.PL.Console.Implementations
{
    public class ConsoleUI : IConsoleUI
    {
        private IConsoleHelper _consoleHelper;
        public ConsoleUI(IConsoleHelper consoleHelper)
        {
            _consoleHelper = consoleHelper;
        }

        public async Task GetAllTodoItems()
        {
            try
            {
                var items = await _consoleHelper.GetAllTodoItems();
                PrintAllTodoItems(items);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public async Task AddTodoItem()
        {
            try
            {
                var item = InputTodoItem();
                await _consoleHelper.AddTodoItem(item);
                System.Console.WriteLine("Задача успешно добавлена!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteTodoItem()
        {
            try
            {
                var items = await _consoleHelper.GetAllTodoItems();
                if (items.Count > 0)
                {
                    PrintAllTodoItems(items);
                    System.Console.Write("Введите номер задачи для удаления: ");
                    var index = InputTodoItemIndex(1, items.Count);
                    await _consoleHelper.DeleteTodoItem(items[index - 1].Id);
                    System.Console.WriteLine("Задача успешно удалена!");
                }
                else
                {
                    System.Console.WriteLine("Вам пока что нечего удалять!");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public async Task SetTodoItemAsDone()
        {
            try
            {
                var items = await _consoleHelper.GetAllTodoItems();
                if (items.Count > 0)
                {
                    PrintAllTodoItems(items);
                    System.Console.Write("Введите номер задачи, которая была выполнена: ");
                    var index = InputTodoItemIndex(1, items.Count);
                    await _consoleHelper.SetTodoItemAsDone(items[index - 1].Id);
                    System.Console.WriteLine("Задача успешно помечена выполненной!");
                }
                else
                {
                    System.Console.WriteLine("У вас пока нет задач!");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public async Task SearchTodoItems()
        {
            try
            {
                var nameSubstr = InputTodoItemNameSubstring();
                var items = await _consoleHelper.SearchTodoItems(nameSubstr);
                System.Console.WriteLine($"Найденные задачи по запросу '{nameSubstr}'");
                PrintAllTodoItems(items);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public async Task SortTodoItemsByPriority(bool isAsc)
        {
            try
            {
                var message = isAsc
                    ? "Задачи, отсортированные по возрастанию приоритета: "
                    : "Задачи, отсортированные по убыванию приоритета: ";
                System.Console.WriteLine(message);
                var sortedAscItems = await _consoleHelper.SortTodoItemsByPriority(isAsc);
                PrintAllTodoItems(sortedAscItems);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private TodoItemToAddDTO InputTodoItem()
        {
            try
            {
                System.Console.Write("Введите имя задачи: ");
                var name = System.Console.ReadLine();
                System.Console.WriteLine();

                System.Console.Write("Введите приоритет задачи (LOW, MEDIUM, HIGH): ");
                var priority = System.Console.ReadLine();
                System.Console.WriteLine();

                System.Console.Write("Введите описание задачи (опционально): ");
                var desc = System.Console.ReadLine();
                System.Console.WriteLine();

                return new TodoItemToAddDTO
                {
                    Name = name,
                    Priority = priority,
                    Text = desc
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int InputTodoItemIndex(int minIndex, int maxIndex)
        {
            var result = int.TryParse(System.Console.ReadLine(), out int index);
            if (result)
            {
                if (index >= minIndex && index <= maxIndex)
                {
                    return index;
                }
                else
                {
                    throw new IndexOutOfRangeException("Выбран неверный номер задачи!");
                }
            }
            else
            {
                throw new ArgumentException("Неверный ввод! Нужно выбрать номер задачи.");
            }
        }

        private string InputTodoItemNameSubstring()
        {
            try
            {
                System.Console.Write("Введите фрагмент названия задачи: ");
                return System.Console.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PrintAllTodoItems(List<TodoItemDTO> todoItems)
        {
            for (int i = 0; i < todoItems.Count; i++)
            {
                System.Console.Write($"{i + 1} : ");
                PrintTodoItem(todoItems[i]);
            }
        }

        private void PrintTodoItem(TodoItemDTO item)
        {
            try
            {
                System.Console.WriteLine($"Название: {item.Name,10} | Описание: {item.Text,35} | Важность: {item.Priority,7} | Сделана: {item.IsDone,5}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
