using FluentValidation;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.FluentValidators
{
	public class CustomerValidator:AbstractValidator<Customer>
	{
        public string NotEmptyMessage { get; set; } = "{PropertyName} alanı boş olamaz";

		public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .EmailAddress().WithMessage("E-mail alanı doğru formatta olmalıdır");
            RuleFor(x => x.Age)
                .NotEmpty().WithMessage(NotEmptyMessage)
                .InclusiveBetween(18, 60).WithMessage("18-60 aralığı olmalıdır");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18) >= x && DateTime.Now.AddYears(-60) <= x;
            }).WithMessage("Yaşınız 18-60 Aralığında Olmalıdır");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("{PropertyName} alanı Erkek=1, Bayan=2 olmnalıdır");
            RuleForEach(x => x.Addresses).SetValidator(new AdressValidator());
        }

    }
}
