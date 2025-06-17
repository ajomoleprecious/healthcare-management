namespace HealthcareManagementSystem.Server.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Doctor()
        {
            
        }

        public Doctor(int doctorId, string firstName, string lastName, string specialization)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
        }
    }

}
