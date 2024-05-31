using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Domain.Appointments;
using Domain.Users.Doctors;
using Persistence;
using Shared.Appointments;
using Shared.Appointments.Timeslots;
using Shared.Users.Patients;
using Domain.Users.Patients;

namespace Services.Appointments;
public class AppointmentService : IAppointmentService
{

	private readonly ApplicationDbContext dbContext;

	public AppointmentService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request)
	{
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		var query = dbContext.Appointments.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Timeslot.Time))  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AppointmentDto.Index
			{
				Id = x.Id,
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Datetime = x.Timeslot.Time,
					Duration = x.Timeslot.Duration,
				},
			})
			.ToListAsync();

		var result = new AppointmentResult.Index
		{
			Appointments = items,
			TotalAmount = items.Count,
		};

		return result;
	}

	public async Task<AppointmentResult.Index> GetAppointmentsFromDoctor(AppointmentRequest.Index request, long doctorId)
	{
		var parsedDate = request.Date != null ? DateOnly.Parse(request.Date) : (DateOnly?)null;
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Appointments)
			.Where(x => parsedDate == null || parsedDate == DateOnly.FromDateTime(x.Timeslot.Time))  //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AppointmentDto.Index
			{
				Id = x.Id,
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Datetime = x.Timeslot.Time,
					Duration = x.Timeslot.Duration,
				},
			})
			.ToListAsync();

		var result = new AppointmentResult.Index
		{
			Appointments = items,
			TotalAmount = items.Count,
		};

		return result;
	}
	
	public async Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId)
	{
		AppointmentDto.Detail? appointment = await dbContext.Appointments
			.Select(x => new AppointmentDto.Detail
			{
				Id = x.Id,
				Patient = new PatientDto.Detail
				{
					Id = x.Patient.Id,
					FirstName = x.Patient.FirstName,
					LastName = x.Patient.LastName,
					BirthDate = x.Patient.BirthDate,
					Email = x.Patient.Email,
					PhoneNumber = x.Patient.PhoneNumber,
					CreatedAt = x.Patient.CreatedAt,
					UpdatedAt = x.Patient.UpdatedAt,
				},
				Timeslot = new TimeslotDto.Index
				{
					Id = x.Timeslot.Id,
					Datetime = x.Timeslot.Time,
					Duration = x.Timeslot.Duration,
				},
				Reason = x.Reason,
				Note = x.Note,
			}).SingleOrDefaultAsync(x => x.Id == appointmentId);

		if (appointment == null)
		{
			throw new EntityNotFoundException(nameof(Appointment), appointmentId);
		}

		return appointment;
	}

    public async Task<long> CreateAsync(AppointmentDto.Mutate model)
    {
		Doctor? doctor = await dbContext.Employees.OfType<Doctor>().SingleOrDefaultAsync(x => x.Id == model.Employee.Id);
		if (doctor is null)
		{
            throw new EntityNotFoundException(nameof(Doctor), model.Employee.Id);
        }

		Patient? patient = await dbContext.Patients.SingleOrDefaultAsync(x => x.Email.Equals(model.Patient.Email));
		if (patient is null)
		{
			patient = new Patient(model.Patient.FirstName!, model.Patient.LastName!, (DateTime)model.Patient.BirthDate!, model.Patient.PhoneNumber!, model.Patient.Email!);
		}

		Timeslot? timeslot = await dbContext.Timeslots.SingleOrDefaultAsync(x => x.Id == model.Timeslot.Id);
        if (timeslot is null)
		{
			throw new EntityNotFoundException(nameof(Timeslot), model.Timeslot.Id);
		}

		Appointment appointment = new(patient, timeslot, model.Reason!, model.Note!);

		doctor.Appointment(appointment);
		await dbContext.SaveChangesAsync();

		return appointment.Id;
    }

	public async Task<AppointmentResult.UltraDetail> GetAllDetailAppointmentsAsync(long doctorId)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Appointments)
			.OrderBy(x => x.Id)
			.Select(x => new AppointmentDto.UltraDetail
			{
				Id = x.Id,
				Patient = new PatientDto.Detail
				{
					Id = x.Patient.Id,
					FirstName = x.Patient.FirstName,
					LastName = x.Patient.LastName,
					BirthDate = x.Patient.BirthDate,
					Email = x.Patient.Email,
					PhoneNumber = x.Patient.PhoneNumber,
					CreatedAt = x.Patient.CreatedAt,
					UpdatedAt = x.Patient.UpdatedAt,
				},
				Timeslot = new TimeslotDto.Detail
				{
					Id = x.Timeslot.Id,
					Start = x.Timeslot.Time,
					End = x.Timeslot.Time.AddMinutes(x.Timeslot.Duration.TotalMinutes),
					Duration = x.Timeslot.Duration,
				},
				Reason = x.Reason,
				Note = x.Note,
			})
			.ToListAsync();

		var result = new AppointmentResult.UltraDetail
		{
			Appointments = items,
			TotalAmount = items.Count,
		};

		return result;
	}
}