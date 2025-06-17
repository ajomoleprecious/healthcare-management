namespace HealthcareManagementSystem.Server.ViewModels
{
    public class UpdateDoctorViewModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public List<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();
    }
}
