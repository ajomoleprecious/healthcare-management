using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.Services;
using HealthcareManagementSystem.Server.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<PatientViewModel>>> GetPatients()
        {
            return await _patientService.GetAllPatientsVMAsync();
        }

        [HttpGet]
        [Route("{patientId}")]
        public async Task<ActionResult<PatientViewModel>> GetPatientById(int patientId)
        {
            try
            {
                return await _patientService.GetPatientByIdVMAsync(patientId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPatient([FromBody] CreatePatientViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth
                };
                await _patientService.AddPatientAsync(patient);
                return CreatedAtAction(nameof(GetPatientById), new { id = patient.PatientId }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{patientId}")]
        public async Task<ActionResult> UpdatePatient(int patientId, [FromBody] UpdatePatientViewModel patient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingPatient = await _patientService.GetPatientByIdAsync(patientId);
                if (existingPatient == null)
                {
                    return NotFound($"Patient with id {patientId} was not found");
                }
                var updatedPatient = new Patient
                {
                    PatientId = patientId,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    Appointments = existingPatient.Appointments
                };
                await _patientService.UpdatePatientAsync(patientId, updatedPatient);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{patientId}")]
        public async Task<ActionResult> DeletePatient(int patientId)
        {
            try
            {
                var existingPatient = await _patientService.GetPatientByIdAsync(patientId);
                if (existingPatient == null)
                {
                    return NotFound($"Patient with id {patientId} was not found");
                }
                await _patientService.DeletePatientAsync(patientId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
