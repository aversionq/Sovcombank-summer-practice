using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.Design;
using TodoList.BLL.Implementations;
using TodoList.BLL.Interfaces;
using TodoList.DAL.Interfaces;
using TodoList.DAL.JsonDAO;
using TodoList.PL.Console.Implementations;
using TodoList.PL.Console.Interfaces;

namespace TodoList.PL.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Использование встроенного внедрения зависимостей.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ITodoListDAO, JsonDAO>();
                    services.AddSingleton<ITodoListBLL, TodoListBusiness>();
                    services.AddSingleton<IConsoleUI, ConsoleUI>();
                    services.AddSingleton<IConsoleHelper, ConsoleHelper>();
                    services.AddSingleton<IConsoleApp, ConsoleApp>();
                })
                .Build();

            var consoleApp = ActivatorUtilities.CreateInstance<ConsoleApp>(host.Services);
            await consoleApp.Run();
        }
    }
}