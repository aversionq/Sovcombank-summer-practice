using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.PL.Console.Interfaces;

namespace TodoList.PL.Console.Implementations
{
    public class ConsoleApp : IConsoleApp
    {
        private IConsoleUI _ui;

        public ConsoleApp(IConsoleUI consoleUI)
        {
            _ui = consoleUI;
        }

        public async Task Run()
        {
            var beginningMessage = $"{Environment.NewLine}Меню программы: {Environment.NewLine}" +
                $"1. Вывести все задачи (без сортировки) {Environment.NewLine}" +
                $"2. Вывести все задачи (сортировка по возрастанию приоритета) {Environment.NewLine}" +
                $"3. Вывести все задачи (сортировка по убыванию приоритета) {Environment.NewLine}" +
                $"4. Добавить новую задачу {Environment.NewLine}" +
                $"5. Удалить задачу {Environment.NewLine}" +
                $"6. Найти задачу по названию {Environment.NewLine}" +
                $"7. Пометить задачу, как 'выполненная' {Environment.NewLine}" +
                $"8. Закрыть программу {Environment.NewLine}";

            while (true)
            {
                System.Console.WriteLine(beginningMessage);
                System.Console.Write("Выберите пункт из меню: ");
                bool choiceResult = int.TryParse(System.Console.ReadLine(), out int choice);

                if (choiceResult)
                {
                    switch (choice)
                    {
                        case 1:
                            await _ui.GetAllTodoItems();
                            break;
                        case 2:
                            await _ui.SortTodoItemsByPriority(true); 
                            break;
                        case 3:
                            await _ui.SortTodoItemsByPriority(false);
                            break;
                        case 4:
                            await _ui.AddTodoItem();
                            break;
                        case 5:
                            await _ui.DeleteTodoItem();
                            break;
                        case 6:
                            await _ui.SearchTodoItems();
                            break;
                        case 7:
                            await _ui.SetTodoItemAsDone();
                            break;
                        case 8:
                            System.Console.WriteLine($"{Environment.NewLine}" +
                                $"Выполняется выход из программы...{Environment.NewLine}");
                            return;
                        default:
                            System.Console.WriteLine($"{Environment.NewLine}wrong option{Environment.NewLine}");
                            break;
                    }
                }
                else
                {
                    System.Console.WriteLine($"{Environment.NewLine}Неверный ввод!" +
                        $" Вам нужно ввести номер опции из меню.{Environment.NewLine}");
                }
            }
        }
    }
}
