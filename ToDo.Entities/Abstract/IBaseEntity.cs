using System;

namespace ToDo.Entities.Abstract
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime InsertedDate { get; set; }
    }
}
