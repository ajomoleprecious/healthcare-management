using HealthcareManagementSystem.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Server.Services
{
    public class InMemoryDB : IDataRepository
    {
        private List<Patient> Patients { get; set; }
        private List<Doctor> Doctors { get; set; }
        private List<Appointment> Appointments { get; set; }

        public InMemoryDB()
        {
            Patients = new List<Patient>
            {
                new Patient(1, "John", "Doe", new DateTime(1985, 5, 15)),
                new Patient(2, "Alice", "Smith", new DateTime(1990, 3, 22)),
                new Patient(3, "Mark", "Johnson", new DateTime(1978, 11, 2))
            };

            Doctors = new List<Doctor>
            {
                new Doctor(1, "Emily", "Brown", "Cardiologist"),
                new Doctor(2, "Michael", "Johnson", "Dermatologist"),
                new Doctor(3, "Sarah", "Lee", "Pediatrician")
            };

            Appointments = new List<Appointment>
            {
                new Appointment(1, 1, 1, new DateTime(2022, 1, 15)),
                new Appointment(2, 2, 2, new DateTime(2022, 2, 20)),
                new Appointment(3, 3, 3, new DateTime(2022, 3, 25))
            };
        }

        public Task<List<Doctor>> GetAllDoctorsAsync()
        {
            foreach (var doctor in Doctors)
            {
                doctor.Appointments = Appointments.Where(a => a.DoctorId == doctor.DoctorId).ToList();

                foreach (var appointment in doctor.Appointments)
                {
                    appointment.Doctor = Doctors.FirstOrDefault(d => d.DoctorId == appointment.DoctorId)
                                         ?? throw new Exception("Doctor not found");
                    appointment.Patient = Patients.FirstOrDefault(p => p.PatientId == appointment.PatientId)
                                          ?? throw new Exception("Patient not found");
                }
            }

            return Task.FromResult(Doctors);
        }


        public Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor != null)
            {
                doctor.Appointments = Appointments.Where(a => a.DoctorId == doctorId).ToList();
                foreach (var appointment in doctor.Appointments)
                {
                    appointment.Doctor = Doctors.FirstOrDefault(d => d.DoctorId == appointment.DoctorId)
                                         ?? throw new Exception("Doctor not found");
                    appointment.Patient = Patients.FirstOrDefault(p => p.PatientId == appointment.PatientId)
                                          ?? throw new Exception("Patient not found");
                }
                return Task.FromResult(doctor);
            }
            throw new Exception("Doctor not found");
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            if (Doctors.Any(d => d.DoctorId == doctor.DoctorId))
            {
                throw new Exception("Doctor with the same id already exists");
            }
            Doctors.Add(new Doctor(Doctors.Count + 1, doctor.FirstName, doctor.LastName, doctor.Specialization));
            return Task.CompletedTask;
        }

        public Task DeleteDoctorAsync(int doctorId)
        {
            var doctor = GetDoctorByIdAsync(doctorId).Result;
            if (doctor != null)
            {
                Doctors.Remove(doctor);
                return Task.CompletedTask;
            }
            throw new Exception("Doctor not found");
        }

        public Task UpdateDoctorAsync(int doctorId, Doctor doctor)
        {
            var existingDoctor = GetDoctorByIdAsync(doctorId).Result;
            if (existingDoctor != null)
            {
                existingDoctor.FirstName = doctor.FirstName;
                existingDoctor.LastName = doctor.LastName;
                existingDoctor.Specialization = doctor.Specialization;
                return Task.CompletedTask;
            }
            throw new Exception("Doctor not found");
        }

        public Task<List<Patient>> GetAllPatientsAsync()
        {
            return Task.FromResult(Patients);
        }

        public Task<Patient> GetPatientByIdAsync(int patientId)
        {
            return Task.FromResult(Patients.FirstOrDefault(p => p.PatientId == patientId) ?? throw new Exception("Patient not found"));
        }

        public Task AddPatientAsync(Patient patient)
        {
            if (Patients.Any(p => p.PatientId == patient.PatientId))
            {
                throw new Exception("Patient with the same id already exists");
            }
            Patients.Add(new Patient(Patients.Count + 1, patient.FirstName, patient.LastName, patient.DateOfBirth));
            return Task.CompletedTask;
        }

        public Task DeletePatientAsync(int patientId)
        {
            var patient = Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient != null)
            {
                Patients.Remove(patient);
                return Task.CompletedTask;
            }
            throw new Exception("Patient not found");
        }

        public Task UpdatePatientAsync(int patientId, Patient patient)
        {
            var existingPatient = Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.DateOfBirth = patient.DateOfBirth;
                return Task.CompletedTask;
            }
            throw new Exception("Patient not found");
        }

        public Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            foreach (var appointment in Appointments)
            {
                appointment.Doctor = Doctors.FirstOrDefault(d => d.DoctorId == appointment.DoctorId)
                                     ?? throw new Exception("Doctor not found");
                appointment.Patient = Patients.FirstOrDefault(p => p.PatientId == appointment.PatientId)
                                      ?? throw new Exception("Patient not found");
            }
            return Task.FromResult(Appointments);
        }

        public Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment != null)
            {
                appointment.Doctor = Doctors.FirstOrDefault(d => d.DoctorId == appointment.DoctorId)
                                     ?? throw new Exception("Doctor not found");
                appointment.Patient = Patients.FirstOrDefault(p => p.PatientId == appointment.PatientId)
                                      ?? throw new Exception("Patient not found");
                return Task.FromResult(appointment);
            }
            throw new Exception("Appointment not found");
        }

        public Task AddAppointmentAsync(Appointment appointment)
        {
            if (Appointments.Any(a => a.AppointmentId == appointment.AppointmentId))
            {
                throw new Exception("Appointment with the same id already exists");
            }
            Appointments.Add(new Appointment(Appointments.Count + 1, appointment.PatientId, appointment.DoctorId, appointment.AppointmentDate));
            return Task.CompletedTask;
        }

        public Task DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment != null)
            {
                Appointments.Remove(appointment);
                return Task.CompletedTask;
            }
            throw new Exception("Appointment not found");
        }

        public Task UpdateAppointmentAsync(int appointmentId, Appointment appointment)
        {
            var existingAppointment = Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (existingAppointment != null)
            {
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.DoctorId = appointment.DoctorId;
                existingAppointment.AppointmentDate = appointment.AppointmentDate;
                return Task.CompletedTask;
            }
            throw new Exception("Appointment not found");
        }
    }
}
