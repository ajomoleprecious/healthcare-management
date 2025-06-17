using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.Services;
using HealthcareManagementSystem.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<DoctorViewModel>>> GetDoctors()
        {
            return await _doctorService.GetAllDoctorsVMAsync();
        }

        [HttpGet]
        [Route("{doctorId}")]
        public async Task<ActionResult<DoctorViewModel>> GetDoctorById(int doctorId)
        {
            try
            {
                return await _doctorService.GetDoctorByIdVMAsync(doctorId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDoctor([FromBody] CreateDoctorViewModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newDoctor = new Doctor
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Specialization = doctor.Specialization
                };
                await _doctorService.AddDoctorAsync(newDoctor);
                return CreatedAtAction(nameof(GetDoctorById), new { doctorId = newDoctor.DoctorId }, newDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{doctorId}")]
        public async Task<ActionResult> UpdateDoctor(int doctorId, [FromBody] UpdateDoctorViewModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingDoctor = await _doctorService.GetDoctorByIdAsync(doctorId);
                if (existingDoctor == null)
                {
                    return NotFound($"Doctor with id {doctorId} was not found");
                }
                var updatedDoctor = new Doctor
                {
                    DoctorId = doctorId,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Specialization = doctor.Specialization,
                    Appointments = existingDoctor.Appointments
                };
                await _doctorService.UpdateDoctorAsync(doctorId, updatedDoctor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{doctorId}")]
        public async Task<ActionResult> DeleteDoctor(int doctorId)
        {
            try
            {
                var existingDoctor = await _doctorService.GetDoctorByIdAsync(doctorId);
                if (existingDoctor == null)
                {
                    return NotFound($"Doctor with id {doctorId} was not found");
                }
                await _doctorService.DeleteDoctorAsync(doctorId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
