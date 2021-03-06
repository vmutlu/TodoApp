using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? LastModificationDate { get; set; }
    }
}
