using FluentValidation;

namespace CCRS.Business.Models.Validations
{
    public class PatientValidation : AbstractValidator<Patient>
    {
        public PatientValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}