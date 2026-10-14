using DentistryAppointmentPrac.Shared.Patients.DTO;
using FluentValidation;

namespace DentistryAppointmentPrac.Shared.Patients.Validators
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientRequest>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(x => x.Cellphone).MinimumLength(10).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.IdNumber).MinimumLength(13).NotEmpty().WithMessage("ID number is required."); 
        }
    }
}
