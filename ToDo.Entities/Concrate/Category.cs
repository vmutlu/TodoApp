using System;
using System.Collections.Generic;
using ToDo.Core.Entities;

namespace ToDo.Entities.Concrate
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Todo> Todos { get; set; }
    }
}
