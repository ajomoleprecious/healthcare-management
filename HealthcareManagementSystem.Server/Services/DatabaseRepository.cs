using HealthcareManagementSystem.Server.Data;
using HealthcareManagementSystem.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Server.Services
{
    public class DatabaseRepository : IDataRepository
    {
        private readonly SqlDbContext _context;

        public DatabaseRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                            .Include(a => a.Doctor)
                            .Include(a => a.Patient)
                            .ToListAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.Include(d => d.Appointments).ThenInclude(a => a.Patient).ToListAsync();
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.Include(p => p.Appointments).ThenInclude(a => a.Doctor).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefaultAsync(a => a.AppointmentId == appointmentId) ?? throw new Exception($"Appointment with id {appointmentId} was not found");
        }

        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            return await _context.Doctors.Include(d => d.Appointments).ThenInclude(a => a.Patient).FirstOrDefaultAsync(d => d.DoctorId == doctorId) ?? throw new Exception($"Doctor with id {doctorId} was not found");
        }

        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {
            return await _context.Patients.Include(p => p.Appointments).ThenInclude(a => a.Doctor).FirstOrDefaultAsync(p => p.PatientId == patientId) ?? throw new Exception($"Patient with id {patientId} was not found");
        }

        public async Task UpdateAppointmentAsync(int appointmentId, Appointment appointment)
        {
            var existingAppointment = await GetAppointmentByIdAsync(appointmentId);
            if (existingAppointment != null)
            {
                _context.Entry(existingAppointment).CurrentValues.SetValues(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDoctorAsync(int doctorId, Doctor doctor)
        {
            var existingDoctor = await GetDoctorByIdAsync(doctorId);
            if (existingDoctor != null)
            {
                _context.Entry(existingDoctor).CurrentValues.SetValues(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePatientAsync(int patientId, Patient patient)
        {
            var existingPatient = await GetPatientByIdAsync(patientId);
            if (existingPatient != null)
            {
                _context.Entry(existingPatient).CurrentValues.SetValues(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
