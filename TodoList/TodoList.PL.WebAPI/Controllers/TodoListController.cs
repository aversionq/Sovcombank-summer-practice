using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.BLL.DTOs;
using TodoList.BLL.Interfaces;
using TodoList.PL.WebAPI.ResponseModels;

namespace TodoList.PL.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private ITodoListBLL _bll;

        public TodoListController(ITodoListBLL todoListBLL)
        {
            _bll = todoListBLL;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItemDTO>>> GetAllTodoItems()
        {
            try
            {
                var items = await _bll.GetAllTodoItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 10000
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItemById(Guid id)
        {
            try
            {
                var item = await _bll.GetTodoItemById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 11000
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTodoItem(TodoItemToAddDTO item)
        {
            try
            {
                await _bll.AddTodoItem(item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 12000
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItem(Guid id)
        {
            try
            {
                await _bll.DeleteTodoItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 13000
                });
            }
        }

        [HttpPatch]
        [Route("setTodoItemAsDone")]
        public async Task<ActionResult> SetTodoItemAsDone(Guid id)
        {
            try
            {
                await _bll.SetTodoItemAsDone(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 14000
                });
            }
        }

        [HttpGet]
        [Route("searchTodoItemsByName")]
        public async Task<ActionResult<List<TodoItemDTO>>> SearchTodoItemsByName(string nameSubstring)
        {
            try
            {
                var matchedItems = await _bll.SearchTodoItems(nameSubstring);
                return Ok(matchedItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 15000
                });
            }
        }

        [HttpGet]
        [Route("sortTodoItemsByPriority")]
        public async Task<ActionResult<List<TodoItemDTO>>> SortTodoItemsByPriority(bool isAsc)
        {
            try
            {
                var sortedItems = await _bll.SortTodoItemsByPriority(isAsc);
                return Ok(sortedItems);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorDescription = ex.Message,
                    ErrorCode = 16000
                });
            }
        }
    }
}
