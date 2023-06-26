using AutoMapper;
using TodoList.BLL.DTOs;
using TodoList.BLL.Interfaces;
using TodoList.DAL.Interfaces;
using TodoList.Entities;

namespace TodoList.BLL.Implementations
{
    public class TodoListBusiness : ITodoListBLL
    {
        private ITodoListDAO _todoListDAO;
        private Mapper _todoItemsMapper;

        public TodoListBusiness(ITodoListDAO todoListDAO)
        {
            _todoListDAO = todoListDAO;
            SetupMappers();
        }

        public async Task AddTodoItem(TodoItemDTO item)
        {
            try
            {
                Enum.Parse<PriorityType>(item.Priority);
                item.Id = Guid.NewGuid();
                var todoItemEntity = _todoItemsMapper.Map<TodoItemDTO, TodoItem>(item);
                await _todoListDAO.AddTodoItem(todoItemEntity);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Неверное значение приоритета задачи!");
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
                await _todoListDAO.DeleteTodoItem(id);
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
                var todoItems = await _todoListDAO.GetAllTodoItems();
                var todoItemsDTO = _todoItemsMapper.Map<List<TodoItem>, List<TodoItemDTO>>(todoItems);
                return todoItemsDTO;
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
                var todoItem = await _todoListDAO.GetTodoItemById(id);
                var todoItemDTO = _todoItemsMapper.Map<TodoItem, TodoItemDTO>(todoItem);
                return todoItemDTO;
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
                var foundTodoItems = await _todoListDAO.SearchTodoItems(nameSubstring);
                var todoItemsDTO = _todoItemsMapper.Map<List<TodoItem>, List<TodoItemDTO>>(foundTodoItems);
                return todoItemsDTO;
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
                await _todoListDAO.SetTodoItemAsDone(id);
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
                var todoItems = await _todoListDAO.GetAllTodoItems();
                todoItems = isAsc 
                    ? todoItems.OrderBy(x => x.Priority).ToList() 
                    : todoItems.OrderByDescending(x => x.Priority).ToList();
                var todoItemsDTO = _todoItemsMapper.Map<List<TodoItem>, List<TodoItemDTO>>(todoItems);
                return todoItemsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetupMappers()
        {
            var todoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TodoItem, TodoItemDTO>().ReverseMap());
            _todoItemsMapper = new Mapper(todoMapperConfig);
        }
    }
}