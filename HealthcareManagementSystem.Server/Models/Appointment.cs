using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.Server.Models
{
    public class Appointment
    {
        [Range(1, int.MaxValue, ErrorMessage = "AppointmentId must be greater than 0")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "PatientId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PatientId must be greater than 0")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "DoctorId must be greater than 0")]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required")]
        [DataType(DataType.Date, ErrorMessage = "AppointmentDate must be a date")]
        public DateTime AppointmentDate { get; set; }

        public Appointment()
        {
        }

        public Appointment(int appointmentId, int patientId, int doctorId, DateTime appointmentDate)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;

            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
        }
    }
}
