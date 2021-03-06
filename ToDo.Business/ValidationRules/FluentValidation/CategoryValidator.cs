using FluentValidation;
using ToDo.Entities.Concrate;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori Adı boş geçilemez");
            RuleFor(t => t.Name).Length(5, 50).WithMessage("Kategori Adı en az 5 karakter en fazla 50 karakter olabilir.");
        }
    }
}
