using FluentValidation;
using System;
using ToDo.Entities.Concrate;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(t => t.Content).NotEmpty().WithMessage("TODO içeriği boş geçilemez");
            RuleFor(t => t.Content).Length(5,1000).WithMessage("TODO içeriği en az 5 karakter en fazla 1000 karakter olabilir."); 
            RuleFor(t => t.CategoryId).NotEmpty().WithMessage("Kategorisi olmayan TODO eklenemez");
            RuleFor(t => t.DueDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Bitiş tarihi bugünden küçük olamaz");
            RuleFor(t => t.ReminMeDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Hatırlatma tarihi bugünden küçük olamaz");
        }
    }
}
