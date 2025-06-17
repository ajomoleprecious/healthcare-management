using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;

namespace HealthcareManagementSystem.Server.Services
{
    public class PatientService : IPatientService
    {
        private IDataRepository _repository;
        public PatientService(IDataRepository repository)
        {
            this._repository = repository;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            try
            {

                await _repository.AddPatientAsync(patient);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add patient", ex);
            }
        }

        public async Task DeletePatientAsync(int patientId)
        {
            try
            {

                await _repository.DeletePatientAsync(patientId);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete patient", ex);
            }
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {

            return await _repository.GetAllPatientsAsync();

        }

        public async Task<List<PatientViewModel>> GetAllPatientsVMAsync()
        {
            var patients = await _repository.GetAllPatientsAsync();

            return patients.Select(p => new PatientViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Appointments = p.Appointments?.Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    DoctorName = a.Doctor != null ? $"{a.Doctor.FirstName} {a.Doctor.LastName}" : "Unknown Doctor",
                    PatientName = a.Patient != null ? $"{a.Patient.FirstName} {a.Patient.LastName}" : "Unknown Patient"
                }).ToList() ?? new List<AppointmentViewModel>()
            }).ToList();
        }

        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {

            return await _repository.GetPatientByIdAsync(patientId);

        }

        public async Task<PatientViewModel> GetPatientByIdVMAsync(int patientId)
        {
            var patient = await _repository.GetPatientByIdAsync(patientId);
            return new PatientViewModel
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Appointments = patient.Appointments?.Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                    PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}"
                }).ToList() ?? new List<AppointmentViewModel>()
            };
        }

        public async Task UpdatePatientAsync(int patientId, Patient patient)
        {
            try
            {

                await _repository.UpdatePatientAsync(patientId, patient);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update patient", ex);
            }
        }
    }
}
