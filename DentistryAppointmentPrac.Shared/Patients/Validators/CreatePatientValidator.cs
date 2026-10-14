using DentistryAppointmentPrac.Shared.Patients.DTO;
using FluentValidation;

namespace DentistryAppointmentPrac.Shared.Patients.Validators
{
    public class CreatePatientValidator : AbstractValidator<PatientRequest>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x=> x.Surname).NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(100).WithMessage("Surname cannot exceed 100 characters.");

            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.Cellphone).NotNull().WithMessage("Cellphone is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid cellphone number format.");

            RuleFor(x => x.IdNumber).NotNull().MinimumLength(13).WithMessage("IdNumber must be at least 13 characters long.")
                .MaximumLength(13).WithMessage("IdNumber cannot exceed 13 characters.");
        }
    }
}
