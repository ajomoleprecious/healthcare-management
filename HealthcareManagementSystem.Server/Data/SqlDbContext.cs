using HealthcareManagementSystem.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Server.Data
{
    public class SqlDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SqlDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public required DbSet<Doctor> Doctors { get; set; }
        public required DbSet<Patient> Patients { get; set; }
        public required DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
