using DentistryAppointmentPrac.Shared.Utilities.Imports.Mappers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DentistryAppointmentPrac.Shared.Utilities.Imports.Validators
{
    public class UserImportValidator : AbstractValidator<UserMapper>
    {
        const string EmailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        const string PhoneNumberAcceptedCharacters = @"^(?=(?:.*\d){7,})(?:\+?\d{1,3}[- .]?)?(?:\(?\d+\)?[- .]?)*\d+$";

        public UserImportValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Email_Address).Matches(EmailPattern).NotEmpty();
            RuleFor(x => x.Display_Name).NotEmpty();
            RuleFor(x => x.User_Type).NotEmpty();
            RuleFor(x => x.ID_Number).Length(13).NotEmpty();
            RuleFor(x => x.Mobile_Number).Matches(PhoneNumberAcceptedCharacters);
        }
    }
}
