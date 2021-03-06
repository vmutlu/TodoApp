using System.Collections.Generic;
using ToDo.Core.Entities;

namespace ToDo.Entities.DTOs
{
    public  class CategoryDTO: IDto
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public List<TodoDTO> Todos { get; set; }
    }
}
