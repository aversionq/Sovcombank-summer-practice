using System.Text.Json;
using System.Text.Json.Serialization;
using TodoList.DAL.Interfaces;
using TodoList.Entities;

namespace TodoList.DAL.JsonDAO
{
    public class JsonDAO : ITodoListDAO
    {
        private string _folderPath;

        public JsonDAO()
        {
            var appDirPath = Directory.GetParent(Environment.CurrentDirectory)
                .Parent.Parent.Parent.Parent;
            _folderPath = appDirPath + @"\JsonTodos\";
            Directory.CreateDirectory(_folderPath);
        }

        public async Task AddTodoItem(TodoItem item)
        {
            var serializedTodoItem = JsonSerializer.Serialize(item);
            var jsonFilePath = GetJsonFilePath(item.Id);

            await File.WriteAllTextAsync(jsonFilePath, serializedTodoItem);
        }

        public void DeleteTodoItem(Guid id)
        {
            if (File.Exists(GetJsonFilePath(id)))
            {
                File.Delete(GetJsonFilePath(id));
            }
            else
            {
                throw new FileNotFoundException($"Не получается найти файл '{id}' в директории '{_folderPath}'");
            }
        }

        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            List<TodoItem> todoItemsList = new List<TodoItem>();
            var filePaths = Directory.GetFiles(_folderPath, "*.json");
            foreach (var path in filePaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(path);
                var todoItem = await GetTodoItemById(Guid.Parse(fileName));
                todoItemsList.Add(todoItem);
            }

            return todoItemsList;
        }

        public async Task<TodoItem> GetTodoItemById(Guid id)
        {
            if (File.Exists(GetJsonFilePath(id)))
            {
                var jsonFilePath = GetJsonFilePath(id);
                var todoItem = JsonSerializer
                    .Deserialize<TodoItem>(await File.ReadAllTextAsync(jsonFilePath));

                return todoItem;
            }
            else
            {
                throw new FileNotFoundException($"Не получается найти файл '{id}' в директории '{_folderPath}'");
            }
        }

        public async Task<List<TodoItem>> SearchTodoItems(string nameSubstring)
        {
            var allTodoItems = await GetAllTodoItems();
            var searchTodoItems = allTodoItems
                .Where(x => x.Name.ToLower().Contains(nameSubstring.ToLower()))
                .ToList();
            return searchTodoItems;
        }

        public async Task SetTodoItemAsDone(Guid id)
        {
            var todoItem = await GetTodoItemById(id);
            todoItem.IsDone = true;
            await File.WriteAllTextAsync(GetJsonFilePath(id), JsonSerializer.Serialize(todoItem));
        }

        private string GetJsonFilePath(Guid itemId) => @$"{_folderPath}{itemId}.json";
    }
}