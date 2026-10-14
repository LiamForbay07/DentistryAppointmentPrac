using DentistryAppointmentPrac.Shared;
using DentistryAppointmentPrac.Shared.DTO;
using DentistryAppointmentPrac.Shared.Patients.BusinessLogic;
using DentistryAppointmentPrac.Shared.Patients.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentistryAppointmentPrac.API.Controllers
{
    [ApiController]
    [Route("patient")]
    [Authorize(Policy = "MultiToken")]
    public class PatientController(IPatientService implementation) : BaseController<IPatientService>(implementation)
    {
        [HttpPost("create-patient")]
        public async Task<ActionResult> CreatePatient(PatientRequest request)
        {
            var result = await implementation.CreatePatient(request);
            return string.IsNullOrEmpty(result) ? BadRequest(result) : Ok(result);
        }

        [HttpPut("update-patient")]
        public async Task<ActionResult> UpdatePatient(UpdatePatientRequest request)
        {
            var result = await implementation.UpdatePatient(request);
            return !result ? BadRequest(result) : Ok(result);
        }

        [HttpDelete("delete-patient/{id}")]
        public async Task<ActionResult> DeletePatient(Guid id)
        {
            var result = await implementation.DeletePatient(id);
            return !result ? BadRequest(result) : Ok(result);
        }

        [HttpPost("get-paged-patients")]
        public async Task<ActionResult<PageData<PatientSummaryResponse?>>> GetPagedPatientsAsync(GetPatientSummaryPagedRequest request)
        {
            var result = await implementation.GetPagedPatientsAsync(request);
            return Ok(result);
        }
    }
}
