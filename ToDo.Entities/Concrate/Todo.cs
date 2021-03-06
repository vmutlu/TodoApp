using System;
using ToDo.Core.Entities;

namespace ToDo.Entities.Concrate
{
    public class Todo : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime? ReminMeDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsFavorite { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
