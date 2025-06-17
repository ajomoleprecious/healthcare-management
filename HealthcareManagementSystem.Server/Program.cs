
using HealthcareManagementSystem.Server.Data;
using HealthcareManagementSystem.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSingleton<IDataRepository, InMemoryDB>();
                builder.Services.AddScoped<IAppointmentService, AppointmentService>();
                builder.Services.AddScoped<IPatientService, PatientService>();
                builder.Services.AddScoped<IDoctorService, DoctorService>();
            }
            else
            {
                builder.Services.AddScoped<IDataRepository, DatabaseRepository>();
                builder.Services.AddScoped<IAppointmentService, AppointmentService>();
                builder.Services.AddScoped<IPatientService, PatientService>();
                builder.Services.AddScoped<IDoctorService, DoctorService>();
                builder.Services.AddDbContext<SqlDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthcare API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
