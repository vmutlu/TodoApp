using FluentValidation;
using System;
using ToDo.Entities.Concrate;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(p => p.Content).NotEmpty().WithMessage("TODO içeriği boş geçilemez");
            RuleFor(p => p.Content).Length(5,1000).WithMessage("TODO içeriği en az 5 karakter en fazla 1000 karakter olabilir."); ;
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategorisi olmayan TODO eklenemez");
            RuleFor(p => p.DueDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Bitiş tarihi bugünden küçük olamaz");
            RuleFor(p => p.ReminMeDate).GreaterThanOrEqualTo(DateTime.Today).WithMessage("Hatırlatma tarihi bugünden küçük olamaz");
        }
    }
}
