namespace HealthcareManagementSystem.Server.ViewModels
{
    public class DoctorViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public ICollection<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();
    }
}
