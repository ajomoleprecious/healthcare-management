using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;

namespace HealthcareManagementSystem.Server.Services
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<List<DoctorViewModel>> GetAllDoctorsVMAsync();
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
        Task<DoctorViewModel> GetDoctorByIdVMAsync(int doctorId);
        Task AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int doctorId);
        Task UpdateDoctorAsync(int doctorId, Doctor doctor);
    }
}
