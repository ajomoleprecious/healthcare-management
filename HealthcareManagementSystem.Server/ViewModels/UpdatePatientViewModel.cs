namespace HealthcareManagementSystem.Server.ViewModels
{
    public class UpdatePatientViewModel
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public List<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();
    }
}
