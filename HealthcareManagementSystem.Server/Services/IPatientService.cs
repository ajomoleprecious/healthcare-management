using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;

namespace HealthcareManagementSystem.Server.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<List<PatientViewModel>> GetAllPatientsVMAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task<PatientViewModel> GetPatientByIdVMAsync(int patientId);
        Task AddPatientAsync(Patient patient);
        Task DeletePatientAsync(int patientId);
        Task UpdatePatientAsync(int patientId, Patient patient);
    }
}
