using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Entities;

namespace TodoList.BLL.DTOs
{
    public class TodoItemToAddDTO
    {
        public string Name { get; set; } = null!;
        public string Priority { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
