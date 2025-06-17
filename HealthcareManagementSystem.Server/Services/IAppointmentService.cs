using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;

namespace HealthcareManagementSystem.Server.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<List<AppointmentViewModel>> GetAllAppointmentsVMAsync();
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<AppointmentViewModel> GetAppointmentByIdVMAsync(int appointmentId);
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
        Task UpdateAppointmentAsync(int appointmentId, Appointment appointment);
    }
}
