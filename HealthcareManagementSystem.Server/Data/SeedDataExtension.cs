using HealthcareManagementSystem.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Server.Data
{
    public static class SeedDataExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                 new(
                doctorId: 1,
                firstName: "Emily",
                lastName: "Brown",
                specialization: "Cardiologist"
            ),
            new(
                doctorId: 2,
                firstName: "Michael",
                lastName: "Johnson",
                specialization: "Dermatologist"
            ),
            new(
                doctorId: 3,
                firstName: "Sarah",
                lastName: "Lee",
                specialization: "Pediatrician"
            )
            );
            modelBuilder.Entity<Patient>().HasData(
                new(
                patientId: 1,
                firstName: "John",
                lastName: "Doe",
                dateOfBirth: new DateTime(1985, 5, 15)
                ),
            new(
                patientId: 2,
                firstName: "Alice",
                lastName: "Smith",
                dateOfBirth: new DateTime(1990, 3, 22)
            ),
            new(
                patientId: 3,
                firstName: "Mark",
                lastName: "Johnson",
                dateOfBirth: new DateTime(1978, 11, 2)
            )
            );
            modelBuilder.Entity<Appointment>().HasData(
                new(
                    appointmentId: 1,
                    patientId: 1,
                    doctorId: 1,
                    appointmentDate: new DateTime(2022, 1, 15)
                ),
                new(
                    appointmentId: 2,
                    patientId: 2,
                    doctorId: 2,
                    appointmentDate: new DateTime(2022, 2, 20)
                ),
                new(
                    appointmentId: 3,
                    patientId: 3,
                    doctorId: 3,
                    appointmentDate: new DateTime(2022, 3, 25)
                )
            );
        }
    }
}
