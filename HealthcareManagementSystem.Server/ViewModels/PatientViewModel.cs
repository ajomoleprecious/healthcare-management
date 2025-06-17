namespace HealthcareManagementSystem.Server.ViewModels
{
    public class PatientViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public ICollection<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();
    }
}
