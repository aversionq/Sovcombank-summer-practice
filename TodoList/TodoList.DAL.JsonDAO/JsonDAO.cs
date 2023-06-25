using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoList.DAL.Interfaces;
using TodoList.Entities;

namespace TodoList.DAL.JsonDAO
{
    public class JsonDAO : ITodoListDAO
    {
        private string _folderPath;
        // Контейнеры для кэша.
        private Dictionary<Guid, WeakReference<TodoItem>> _cache = new();

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

            // Кэширование.
            if (!_cache.ContainsKey(item.Id))
            {
                _cache.Add(item.Id, new WeakReference<TodoItem>(item));
            }

            await File.WriteAllTextAsync(jsonFilePath, serializedTodoItem);
        }

        public void DeleteTodoItem(Guid id)
        {
            // Удаление из кэша.
            if (_cache.ContainsKey(id))
            {
                _cache.Remove(id);
            }

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

            return todoItemsList.OrderBy(x => x.IsDone).ToList();
        }

        public async Task<TodoItem> GetTodoItemById(Guid id)
        {
            // Проверка, есть ли объект в кэше.
            TodoItem item;
            WeakReference<TodoItem> weakRef;
            if (_cache.TryGetValue(id, out weakRef) && weakRef.TryGetTarget(out item))
            {
                return item;
            }

            if (File.Exists(GetJsonFilePath(id)))
            {
                var jsonFilePath = GetJsonFilePath(id);
                var todoItem = JsonSerializer
                    .Deserialize<TodoItem>(await File.ReadAllTextAsync(jsonFilePath));
                
                // Кэширование.
                if (!_cache.ContainsKey(id))
                {
                    _cache.Add(id, new WeakReference<TodoItem>(todoItem));
                }

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
            // Кэширование
            if (_cache.ContainsKey(id))
            {
                _cache[id] = new WeakReference<TodoItem>(todoItem);
            }
            else
            {
                _cache.Add(id, new WeakReference<TodoItem>(todoItem));
            }

            await File.WriteAllTextAsync(GetJsonFilePath(id), JsonSerializer.Serialize(todoItem));
        }

        private string GetJsonFilePath(Guid itemId) => @$"{_folderPath}{itemId}.json";
    }
}