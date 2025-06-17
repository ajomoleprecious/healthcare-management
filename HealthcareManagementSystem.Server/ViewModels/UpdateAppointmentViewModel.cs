namespace HealthcareManagementSystem.Server.ViewModels
{
    public class UpdateAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
