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
                throw new FileNotFoundException($"Can't find file '{id}' in directory '{_folderPath}'");
            }
        }

        public async Task<List<TodoItem>> GetAllTodoItems()
        {
            List<TodoItem> todoItemsList = new List<TodoItem>();
            foreach (var todoItemFile in Directory.GetFiles(_folderPath, "*.json"))
            {
                var jsonFile = await File.ReadAllTextAsync(todoItemFile);
                var todoItem = JsonSerializer.Deserialize<TodoItem>(jsonFile);
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
                throw new FileNotFoundException($"Can't find file '{id}' in directory '{_folderPath}'");
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
        }

        private string GetJsonFilePath(Guid itemId) => @$"{_folderPath}{itemId}.json";
    }
}