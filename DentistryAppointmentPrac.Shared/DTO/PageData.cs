using System;
using System.Collections.Generic;
using System.Text;

namespace DentistryAppointmentPrac.Shared.DTO
{
    public class PageData<TSummary>
    {
        public IEnumerable<TSummary> Data { get; set; } = [];
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
