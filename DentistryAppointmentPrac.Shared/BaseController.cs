using DentistryAppointmentPrac.Shared.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.Shared
{
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : IBaseImplementation
    {
        protected readonly T implementation;
        public BaseController(T _implementation) {
            implementation = _implementation;
        }
    }
}
