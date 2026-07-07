using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.OLlamAi.GuardRails
{
    public static class InputGuardRails
    {
        public static void Validate(string prompt)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                throw new InvalidOperationException("The request is empty");
            }

            if(prompt.Length > 10000)
            {
                throw new InvalidOperationException("The request is too large");
            }
        }
    }
}
