using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.Services;
using HealthcareManagementSystem.Server.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<AppointmentViewModel>>> GetAppointments()
        {
            return await _appointmentService.GetAllAppointmentsVMAsync();
        }

        [HttpGet]
        [Route("{appointmentId}")]
        public async Task<ActionResult<AppointmentViewModel>> GetAppointmentById(int appointmentId)
        {
            try
            {
                return await _appointmentService.GetAppointmentByIdVMAsync(appointmentId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAppointment([FromBody] CreateAppointmentViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var appointment = new Appointment
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    AppointmentDate = model.AppointmentDate
                };

                await _appointmentService.AddAppointmentAsync(appointment);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{appointmentId}")]
        public async Task<ActionResult> UpdateAppointment(int appointmentId, [FromBody] UpdateAppointmentViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
                if (existingAppointment == null)
                {
                    return NotFound();
                }
                var appointment = new Appointment
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    AppointmentDate = model.AppointmentDate
                };
                await _appointmentService.UpdateAppointmentAsync(appointmentId, appointment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{appointmentId}")]
        public async Task<ActionResult> DeleteAppointment(int appointmentId)
        {
            try
            {
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
                if (existingAppointment == null)
                {
                    return NotFound();
                }

                await _appointmentService.DeleteAppointmentAsync(appointmentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
