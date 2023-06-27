using Microsoft.EntityFrameworkCore;
using TodoList.BLL.Implementations;
using TodoList.BLL.Interfaces;
using TodoList.DAL.Interfaces;
using TodoList.DAL.JsonDAO;
using TodoList.DAL.SqlDAO;

namespace TodoList.PL.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ITodoListDbContext, TodoListDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
            });
            builder.Services.AddScoped<ITodoListDAO, SqlDAO>();
            builder.Services.AddScoped<ITodoListBLL, TodoListBusiness>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}