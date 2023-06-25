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
                    _consoleHelper.DeleteTodoItem(items[index - 1].Id);
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

        private TodoItemDTO InputTodoItem()
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

                return new TodoItemDTO
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
                System.Console.WriteLine($"Название: {item.Name,10} | Описание: {item.Text,20} | Важность: {item.Priority,7} | Сделана: {item.IsDone,5}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task Run()
        //{
        //    var beginningMessage = $"Меню программы: {Environment.NewLine}" +
        //        $"1. Вывести все задачи (без сортировки) {Environment.NewLine}" +
        //        $"2. Вывести все задачи (сортировка по возрастанию приоритета) {Environment.NewLine}" +
        //        $"3. Вывести все задачи (сортировка по убыванию приоритета) {Environment.NewLine}" +
        //        $"4. Добавить новую задачу {Environment.NewLine}" +
        //        $"5. Удалить задачу {Environment.NewLine}" +
        //        $"6. Найти задачу по названию {Environment.NewLine}" +
        //        $"7. Пометить задачу, как 'выполненная' {Environment.NewLine}" +
        //        $"8. Закрыть программу {Environment.NewLine}";

        //    while (true)
        //    {
        //        System.Console.WriteLine(beginningMessage);
        //        System.Console.Write("Выберите пункт из меню: ");
        //        bool choiceResult = int.TryParse(System.Console.ReadLine(), out int choice);

        //        if (choiceResult)
        //        {
        //            switch (choice)
        //            {
        //                case 1:
        //                    {
        //                        try
        //                        {
        //                            var items = await _consoleHelper.GetAllTodoItems();
        //                            PrintAllTodoItems(items);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            System.Console.WriteLine(ex.Message);
        //                        }
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        try
        //                        {
        //                            var items = await _consoleHelper.GetAllTodoItems();
        //                            var sortedAscItems = await _consoleHelper.SortTodoItemsByPriority(true, items);
        //                            PrintAllTodoItems(sortedAscItems);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            System.Console.WriteLine(ex.Message);
        //                        }
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        try
        //                        {
        //                            var items = await _consoleHelper.GetAllTodoItems();
        //                            var sortedDescItems = await _consoleHelper.SortTodoItemsByPriority(false, items);
        //                            PrintAllTodoItems(sortedDescItems);
        //                        }
        //                        catch (Exception ex) 
        //                        { 
        //                            System.Console.WriteLine(ex.Message); 
        //                        }
        //                        break;
        //                    }
        //                case 4:
        //                    try
        //                    {
        //                        var item = InputTodoItem();
        //                        await _consoleHelper.AddTodoItem(item);
        //                        System.Console.WriteLine("Задача успешно добавлена!");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        System.Console.WriteLine(ex.Message);
        //                    }
        //                    break;
        //                case 5:
        //                    {
        //                        try
        //                        {
        //                            var items = await _consoleHelper.GetAllTodoItems();
        //                            if (items.Count > 0)
        //                            {
        //                                PrintAllTodoItems(items);
        //                                System.Console.Write("Введите номер задачи для удаления: ");
        //                                var index = InputTodoItemIndex(1, items.Count);
        //                                _consoleHelper.DeleteTodoItem(items[index - 1].Id);
        //                                System.Console.WriteLine("Задача успешно удалена!");
        //                            }
        //                            else
        //                            {
        //                                System.Console.WriteLine("Вам пока что нечего удалять!");
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            System.Console.WriteLine(ex.Message);
        //                        }
        //                        break;
        //                    }
        //                case 6:
        //                    {
        //                        try
        //                        {
        //                            var nameSubstr = InputTodoItemNameSubstring();
        //                            var items = await _consoleHelper.SearchTodoItems(nameSubstr);
        //                            PrintAllTodoItems(items);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            System.Console.WriteLine(ex.Message);
        //                        }
        //                        break;
        //                    }
        //                case 7:
        //                    {
        //                        try
        //                        {
        //                            var items = await _consoleHelper.GetAllTodoItems();
        //                            if (items.Count > 0)
        //                            {
        //                                PrintAllTodoItems(items);
        //                                System.Console.Write("Введите номер задачи, которая была выполнена: ");
        //                                var index = InputTodoItemIndex(1, items.Count);
        //                                await _consoleHelper.SetTodoItemAsDone(items[index - 1].Id);
        //                                System.Console.WriteLine("Задача успешно помечена выполненной!");
        //                            }
        //                            else
        //                            {
        //                                System.Console.WriteLine("У вас пока нет задач!");
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            System.Console.WriteLine(ex.Message);
        //                        }
        //                        break;
        //                    }
        //                case 8:
        //                    System.Console.WriteLine($"{Environment.NewLine}" +
        //                        $"Выполняется выход из программы...{Environment.NewLine}");
        //                    return;
        //                default:
        //                    System.Console.WriteLine($"{Environment.NewLine}wrong option{Environment.NewLine}");
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            System.Console.WriteLine($"{Environment.NewLine}Неверный ввод!" +
        //                $" Вам нужно ввести номер опции из меню.{Environment.NewLine}");
        //        }
        //    }
        //}
    }
}
