using FluentValidation;

namespace CCRS.Business.Models.Validations
{
    public class AppointmentValidation : AbstractValidator<Appointment>
    {
        public AppointmentValidation()
        {
            //RuleFor(c => c.Patient)
            //    .NotEmpty();
        }
    }
}