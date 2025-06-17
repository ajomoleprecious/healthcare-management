namespace HealthcareManagementSystem.Server.ViewModels
{
    public class CreatePatientViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
