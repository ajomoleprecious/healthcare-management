using HealthcareManagementSystem.Server.Data;
using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Server.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IDataRepository _dataRepository;

        public AppointmentService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            try
            {
                await _dataRepository.AddAppointmentAsync(appointment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add appointment: {ex.Message}");
            }
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            try
            {

                await _dataRepository.DeleteAppointmentAsync(appointmentId);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete appointment: {ex.Message}");
            }
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {

            return await _dataRepository.GetAllAppointmentsAsync();

        }

        public async Task<List<AppointmentViewModel>> GetAllAppointmentsVMAsync()
        {
            var appointments = await _dataRepository.GetAllAppointmentsAsync();

            return appointments.Select(a => new AppointmentViewModel
            {
                
                AppointmentId = a.AppointmentId,
                AppointmentDate = a.AppointmentDate,
                DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}"
            }).ToList();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {

            return await _dataRepository.GetAppointmentByIdAsync(appointmentId) ?? throw new Exception($"Appointment with id {appointmentId} was not found");

        }

        public async Task<AppointmentViewModel> GetAppointmentByIdVMAsync(int appointmentId)
        {
            var appointment = await _dataRepository.GetAppointmentByIdAsync(appointmentId);
            return new AppointmentViewModel
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                DoctorName = $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}",
                PatientName = $"{appointment.Patient.FirstName} {appointment.Patient.LastName}"
            };
        }

        public async Task UpdateAppointmentAsync(int appointmentId, Appointment appointment)
        {
            try
            {

                await _dataRepository.UpdateAppointmentAsync(appointmentId, appointment);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update appointment: {ex.Message}");
            }
        }
    }
}
