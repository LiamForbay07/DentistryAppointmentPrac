using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DTO
{
    public class PageRequest
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; } = 100;
        public string Search { get; set; } = string.Empty;
    }
}
