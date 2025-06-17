using HealthcareManagementSystem.Server.Data;
using HealthcareManagementSystem.Server.Models;
using HealthcareManagementSystem.Server.ViewModels;

namespace HealthcareManagementSystem.Server.Services
{
    public class DoctorService : IDoctorService
    {
        private IDataRepository _repository;
        public DoctorService(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            try
            {
                await _repository.AddDoctorAsync(doctor);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add doctor", ex);
            }
        }

        public async Task DeleteDoctorAsync(int doctorId)
        {
            try
            {
                await _repository.DeleteDoctorAsync(doctorId);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete doctor", ex);
            }
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {

            return await _repository.GetAllDoctorsAsync();


        }

        public async Task<List<DoctorViewModel>> GetAllDoctorsVMAsync()
        {
            var doctors = await _repository.GetAllDoctorsAsync();

            return doctors.Select(d => new DoctorViewModel
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                Appointments = d.Appointments?.Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    DoctorName = a.Doctor != null ? $"{a.Doctor.FirstName} {a.Doctor.LastName}" : "Unknown Doctor",
                    PatientName = a.Patient != null ? $"{a.Patient.FirstName} {a.Patient.LastName}" : "Unknown Patient"
                }).ToList() ?? new List<AppointmentViewModel>()
            }).ToList();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {

            return await _repository.GetDoctorByIdAsync(doctorId) ?? throw new Exception($"Doctor with id {doctorId} was not found");


        }

        public async Task<DoctorViewModel> GetDoctorByIdVMAsync(int doctorId)
        {
            var doctor = await _repository.GetDoctorByIdAsync(doctorId);
            return new DoctorViewModel
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                Appointments = doctor.Appointments?.Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDate = a.AppointmentDate,
                    DoctorName = a.Doctor != null ? $"{a.Doctor.FirstName} {a.Doctor.LastName}" : "Unknown Doctor",
                    PatientName = a.Patient != null ? $"{a.Patient.FirstName} {a.Patient.LastName}" : "Unknown Patient"
                }).ToList() ?? new List<AppointmentViewModel>()
            };
        }

        public async Task UpdateDoctorAsync(int doctorId, Doctor doctor)
        {
            try
            {

                await _repository.UpdateDoctorAsync(doctorId, doctor);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update doctor", ex);
            }
        }
    }
}
