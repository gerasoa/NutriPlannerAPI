using FluentValidation;

namespace CCRS.Business.Models.Validations
{
    public class DoctorValidation : AbstractValidator<Doctor>
    {
        public DoctorValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}