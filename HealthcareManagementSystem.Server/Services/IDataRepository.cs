using HealthcareManagementSystem.Server.Models;

namespace HealthcareManagementSystem.Server.Services
{
    public interface IDataRepository
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
        Task AddDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int doctorId);
        Task UpdateDoctorAsync(int doctorId, Doctor doctor);
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task AddPatientAsync(Patient patient);
        Task DeletePatientAsync(int patientId);
        Task UpdatePatientAsync(int patientId, Patient patient);
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task AddAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
        Task UpdateAppointmentAsync(int appointmentId, Appointment appointment);
    }
}
