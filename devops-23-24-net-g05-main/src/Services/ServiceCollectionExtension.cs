using Microsoft.Extensions.DependencyInjection;
using Services.Appointments;
using Services.EyeConditions;
using Services.Users.Doctors;
using Services.Users.Team;
using Shared.Appointments;
using Shared.Appointments.Timeslots;
using Shared.EyeConditions;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Employees;
using Shared.Users.Doctors.Specializations;
using Shared.Users.Patients;
using Shared.Articles;
using Services.Articles;
using Services.Files;
using Shared.Users.Team.Doctors;
using Services.EyeConditions;
using Services.Users.Patients;
using Shared.Users.Teams.Groups;
using Services.Users.Team;
using Shared.Users.Teams.Biographies;

namespace Services;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds all services to the DI container.
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IEyeConditionService, EyeConditionService>();
		services.AddScoped<ISymptomService, SymptomService>();
		services.AddScoped<ITimeslotService, TimeslotService>();
		services.AddScoped<IAppointmentService, AppointmentService>();
		services.AddScoped<IAvailabilityService, AvailabilityService>();
		services.AddScoped<IPatientService, PatientService>();
		services.AddScoped<ISpecializationService, SpecializationService>();
		services.AddScoped<IEmployeeService, EmployeeService>();
		services.AddScoped<IDoctorService, DoctorService>();	
		services.AddScoped<IGroupService, GroupService>();
		services.AddScoped<IBioService, BioService>();
			
		services.AddScoped<IStorageService,BlobStorageService>();
        services.AddScoped<IArticleService, ArticleService>();
		return services;
	}
}