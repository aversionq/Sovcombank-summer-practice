using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.Design;
using TodoList.BLL.Implementations;
using TodoList.BLL.Interfaces;
using TodoList.DAL.Interfaces;
using TodoList.DAL.JsonDAO;
using TodoList.DAL.SqlDAO;
using TodoList.PL.Console.Implementations;
using TodoList.PL.Console.Interfaces;

namespace TodoList.PL.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            // Использование встроенного внедрения зависимостей.
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    context.Configuration = builder.Build();
                    services.AddLogging(b =>
                    {
                        b.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
                    });
                    services.AddSingleton<ITodoListDAO, SqlDAO>();
                    services.AddScoped<ITodoListBLL, TodoListBusiness>();
                    services.AddScoped<IConsoleUI, ConsoleUI>();
                    services.AddScoped<IConsoleHelper, ConsoleHelper>();
                    services.AddScoped<IConsoleApp, ConsoleApp>();
                    services.AddDbContext<ITodoListDbContext, TodoListDbContext>(options =>
                    {
                        options.UseSqlite(context.Configuration.GetConnectionString("SQLite"));
                    });
                })
                .Build();

            var consoleApp = ActivatorUtilities.CreateInstance<ConsoleApp>(host.Services);
            await consoleApp.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.ToString())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}