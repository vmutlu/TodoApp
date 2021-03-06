using System;
using ToDo.Core.Entities;

namespace ToDo.Entities.DTOs
{
    public class TodoDTO: IDto
    { 
        public string Content { get; set; }

        public DateTime? ReminMeDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsFavorite { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual CategoryDTO Category { get; set; }
    }
}
