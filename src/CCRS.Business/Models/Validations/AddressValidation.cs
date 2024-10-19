using FluentValidation;

namespace CCRS.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty();
        }
    }
}