namespace HealthcareManagementSystem.Server.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Patient()
        {
            
        }

        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }

}
