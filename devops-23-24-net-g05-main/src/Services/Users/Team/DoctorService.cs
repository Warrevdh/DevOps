using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Specializations;
using Shared.Users.Team.Doctors;
using Shared.Users.Teams.Groups;

namespace Services.Users.Team;

public class DoctorService : IDoctorService
{
	private readonly ApplicationDbContext dbContext;

	public DoctorService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<DoctorResult.Index> GetIndexAsync(DoctorRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.Name))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.Name));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new DoctorDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Specializations = x.Specializations.Select(x => new SpecializationDto.Index
				{
					Id = x.Id,
					Name = x.Name,
				}),
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new DoctorResult.Index
		{
			Doctors = items,
			TotalAmount = totalAmount,
		};

		return result;
	}

	public async Task<DoctorDto.Detail> GetDetailAsync(long doctorId)
	{
		DoctorDto.Detail? doctor = await dbContext.Employees.OfType<Doctor>().Select(x => new DoctorDto.Detail
		{
			Id= x.Id,
			Firstname= x.FirstName,
			Lastname= x.LastName,
			Birthdate = x.Birthdate,
			Email = x.Email,
			Phonenumber = x.PhoneNumber,
			Group = new GroupDto.Index
			{
				Id = x.Group.Id,
				Name = x.Group.Name,
				Sequence = x.Group.Sequence,
			},
			Specializations = x.Specializations.Select(x => new SpecializationDto.Index
			{
				Id = x.Id,
				Name = x.Name,
			}),
			Availabilities = x.Availabilities.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			}),
			Timeslots = x.Timeslots.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Datetime = x.Time,
				Duration = x.Duration,
			})
		}).SingleOrDefaultAsync(x => x.Id == doctorId);

		if (doctor == null)
		{
			throw new EntityNotFoundException(nameof(doctor), doctorId);
		}

		return doctor;
	}

	public async Task<DoctorDto.Detail> GetByEmailAsync(string email)
	{
		DoctorDto.Detail? doctor = await dbContext.Employees.OfType<Doctor>().Select(x => new DoctorDto.Detail
		{
			Id = x.Id,
			Firstname = x.FirstName,
			Lastname = x.LastName,
			Birthdate = x.Birthdate,
			Email = x.Email,
			Phonenumber = x.PhoneNumber,
			Group = new GroupDto.Index
			{
				Id = x.Group.Id,
				Name = x.Group.Name,
				Sequence = x.Group.Sequence,
			},
			Specializations = x.Specializations.Select(x => new SpecializationDto.Index
			{
				Id = x.Id,
				Name = x.Name,
			}),
			Availabilities = x.Availabilities.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			}),
			Timeslots = x.Timeslots.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Datetime = x.Time,
				Duration = x.Duration,
			})
		}).SingleOrDefaultAsync(x => x.Email!.Equals(email));

		if (doctor == null)
		{
			throw new EntityNotFoundException(nameof(doctor), email);
		}

		return doctor;
	}
}
