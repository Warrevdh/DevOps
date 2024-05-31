using Bogus;
using Domain.Users.Employees;
using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Biographies;
using Shared.Users.Teams.Groups;
using Domain.Appointments;
using Services.Files;
using Domain.Files;
using System.Numerics;
using Domain.EyeConditions;

namespace Services.Users.Team;

public class EmployeeService : IEmployeeService
{

	private readonly ApplicationDbContext dbContext;
    private readonly IStorageService storageService;

    public EmployeeService(ApplicationDbContext dbContext, IStorageService storageService)
	{
		this.dbContext = dbContext;
		this.storageService = storageService;
	}

	public async Task<EmployeeResult.Index> GetIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Image = x.Image,
				Birthdate = x.Birthdate,
				Phone = x.PhoneNumber,
				Email = x.Email,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}
	
	public async Task<EmployeeResult.Index> GetDoctorsIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
				Image = x.Image,
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}

	public async Task<EmployeeResult.Index> GetAssistantsIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Assistant>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
				Image = x.Image,
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}

	public async Task<EmployeeResult.Index> GetSecretaryIndexAsync(EmployeeRequest.Index request)
	{
		var query = dbContext.Employees.OfType<Secretary>().AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.SearchName))
		{
			query = query.Where(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(request.SearchName));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new EmployeeDto.Index
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Group = new GroupDto.Index
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence,
				},
				Image = x.Image,
			})
			.ToListAsync();

		var result = new EmployeeResult.Index
		{
			Employees = items,
			TotalAmount = totalAmount
		};

		return result;
	}
    public async Task<EmployeeDto.Detail> GetDetailAsync(long employeeId)
    {
        EmployeeDto.Detail? employee = await dbContext.Employees.Select(x => new EmployeeDto.Detail
        {
            Id = x.Id,
            Firstname = x.FirstName,
            Lastname = x.LastName,
            Birthdate = x.Birthdate,
            Image = x.Image,
            Email = x.Email,
            Phonenumber = x.PhoneNumber,
            Availabilities = x.Availabilities.Select(x => new AvailabilityDto.Index
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            }),
            Group = new GroupDto.Index
            {
                Id = x.Group.Id,
                Name = x.Group.Name,
                Sequence = x.Group.Sequence,
            },
        }).SingleOrDefaultAsync(x => x.Id == employeeId);

        if (employee == null)
        {
            throw new EntityNotFoundException(nameof(employee), employeeId);
        }

        return employee;
    }

    public async Task EditAsync(long employeeId, EmployeeDto.Mutate model)
	{
		Employee? employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);

		if(employee is null)
		{
			throw new EntityNotFoundException(nameof(Employee), employeeId);
		}

		Group? group;
		if (model.Group is not null)
		{
			group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == model.Group.Id);
		} else
		{
			throw new ApplicationException("There is no group associated with the changed employee");
		}

		if (group is null)
		{
			throw new EntityNotFoundException(nameof(Group), model.Group.Id);
		}

/*        Image image = new Image(storageService.BasePath, model.Image!);

        if (employee.Image != image.FileUri.ToString())
        {
			employee.Image = image.FileUri.ToString();
        }*/


        employee.FirstName = model.Firstname!;
		employee.LastName = model.Lastname!;
		employee.Birthdate = (DateTime)model.Birthdate!;
		employee.Email = model.Email!;
		employee.PhoneNumber = model.Phonenumber!;
		employee.Group = group;
		employee.Image = model.Image!;

		await dbContext.SaveChangesAsync();
	}

	//public async Task<long> CreateAsync(EmployeeDto.Mutate model)
	//{
	//	Group? group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == model.Group!.Id);

	//	if (group is null)
	//	{
	//		throw new EntityNotFoundException(nameof(Group), model.Group!.Id);
	//	}


	//	Employee emp;

	//	if (model.Type!.Equals("Doctor"))
	//	{
	//		emp = new Doctor(model.Firstname!, model.Lastname!, (DateTime)model.Birthdate!, model.Phonenumber!, model.Email!, group, model.Image!);
	//	}
	//	else if (model.Type!.Equals("Secretary"))
	//	{
	//		emp = new Secretary(model.Firstname!, model.Lastname!, (DateTime)model.Birthdate!, model.Phonenumber!, model.Email!, group, model.Image!);
	//	}
	//	else if (model.Type!.Equals("Assistant"))
	//	{
	//		emp = new Assistant(model.Firstname!, model.Lastname!, (DateTime)model.Birthdate!, model.Phonenumber!, model.Email!, group, model.Image!);
	//	}
	//	else
	//	{
	//		throw new ApplicationException($"There is no associated type for employee type {model.Type}");
	//	}

	//	dbContext.Employees.Add(emp);
	//	await dbContext.SaveChangesAsync();

	//	return emp.Id;
	//}

	public async Task ChangeGroupAsync(long employeeId, long groupId)
	{
		Group? group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);
		if (group is null)
		{
			throw new EntityNotFoundException(nameof(Group), groupId);
		}

		Employee? employee = await dbContext.Employees.Include(x => x.Group).SingleOrDefaultAsync(x => x.Id == employeeId);
		if (employee is null)
		{
			throw new EntityNotFoundException(nameof(Employee), employeeId);
		}

		if (employee.Group.Id != groupId)
		{
			employee.Group = group;
			await dbContext.SaveChangesAsync();
		}
	}

    public async Task<EmployeeResult.Create> CreateAsync(EmployeeDto.Mutate model)
    {
        if (await dbContext.Employees.AnyAsync(x => x.Email == model.Email	))
            throw new EntityAlreadyExistsException(nameof(Employee), nameof(Employee.Email), model.Email);

        Image image = new Image(storageService.BasePath, model.Image!);
        
        Group group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == model.Group.Id);

		EmployeeResult.Create res;

		if(model.Function == "Dokter")
		{
			Doctor doctor = new Doctor(model.Firstname, model.Lastname, (DateTime)model.Birthdate!, model.Phonenumber, model.Email, group, image.FileUri.ToString());
			dbContext.Employees.Add(doctor);
            await dbContext.SaveChangesAsync();
            res = new()
			{
				EmployeeId = doctor.Id,
				Firstname = doctor.FirstName,
				Lastname = doctor.LastName,
				BirthDate = doctor.Birthdate,
                UploadUri = storageService.GenerateImageUploadSas(image).ToString(),
				Email = doctor.Email,
				Phone = doctor.PhoneNumber
			};
		}
		else if (model.Function == "Assistent") {
			Assistant assistent = new Assistant(model.Firstname, model.Lastname, (DateTime)model.Birthdate, model.Phonenumber, model.Email, group, image.FileUri.ToString());
            //assistent.Image = image.FileUri.ToString();
            dbContext.Employees.Add(assistent);
            await dbContext.SaveChangesAsync();
            res = new()
            {
                EmployeeId = assistent.Id,
                Firstname = assistent.FirstName,
                Lastname = assistent.LastName,
                BirthDate = assistent.Birthdate,
                UploadUri = storageService.GenerateImageUploadSas(image).ToString(),
                Email = assistent.Email,
                Phone = assistent.PhoneNumber
            };
        }
		else
		{
			Secretary secretariaat = new Secretary(model.Firstname, model.Lastname, (DateTime)model.Birthdate!, model.Phonenumber, model.Email, group, image.FileUri.ToString());
			//secretariaat.Image = image.FileUri.ToString();
            dbContext.Employees.Add(secretariaat);
            await dbContext.SaveChangesAsync();
            res = new()
            {
                EmployeeId = secretariaat.Id,
                Firstname = secretariaat.FirstName,
                Lastname = secretariaat.LastName,
                BirthDate = secretariaat.Birthdate,
                UploadUri = storageService.GenerateImageUploadSas(image).ToString(),
                Email = secretariaat.Email,
                Phone = secretariaat.PhoneNumber
            };
        }
        return res;
    }

    public async Task DeleteAsync(long id)
    {
        Employee? employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == id);

        if (employee is null)
            throw new EntityNotFoundException(nameof(EyeCondition), id);

        dbContext.Employees.Remove(employee);

        await dbContext.SaveChangesAsync();
    }

    public async Task<EmployeeDto.Mutate> GetMutateAsync(long employeeId)
    {
        EmployeeDto.Mutate? employeeDto = await dbContext.Employees
			.Select(x => new EmployeeDto.Mutate()
			{
				Id = x.Id,
				Firstname = x.FirstName,
				Lastname = x.LastName,
				Birthdate = x.Birthdate,
				Email = x.Email,
				Image = x.Image,
				Group = new GroupDto.Index()
				{
					Id = x.Group.Id,
					Name = x.Group.Name,
					Sequence = x.Group.Sequence
				},
				Phonenumber = x.PhoneNumber,
			}).SingleOrDefaultAsync(a => a.Id == employeeId);

		if (employeeDto is null) throw new EntityNotFoundException(nameof(Employee), employeeId);
		
		return employeeDto;
    }

    public async Task AddDailyAvailabilitiesAsync(long employeeId, EmployeeDto.MutateAvailabilityDaily model)
    {
		Doctor? employee = await dbContext.Employees.OfType<Doctor>().SingleOrDefaultAsync(x => x.Id == employeeId);

		if (employee is null) {
			throw new EntityNotFoundException(nameof(Doctor), employeeId);
		}

		DateTime start = model.Availability.StartDate;
		DateTime end = model.Availability.EndDate;

		while(start.Date <= end.Date)
		{
			employee.Availability(new Availability(start, start.Date.Add(end.TimeOfDay)));

			List<Timeslot> slots = new List<Timeslot>();
			foreach(var ts in model.Timeslots)
			{
				slots.Add(
					new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
				);
			}

			employee.Timeslot(slots);
			start = start.AddDays(1);
		}

		await dbContext.SaveChangesAsync();
    }

    public async Task AddWeeklyAvailabilitiesAsync(long employeeId, EmployeeDto.MutateAvailabilityWeekly model)
    {
		Doctor? employee = await dbContext.Employees.OfType<Doctor>().SingleOrDefaultAsync(x => x.Id == employeeId);

		if (employee is null)
		{
			throw new EntityNotFoundException(nameof(Doctor), employeeId);
		}

		DateTime start = model.Availability.StartDate;
		DateTime end = model.Availability.EndDate;

		while (start.Date <= end.Date)
		{
			switch (start.DayOfWeek)
			{
				case DayOfWeek.Monday:
					if (model.TimeslotsMo.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsMo.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsMo.Last().Datetime.TimeOfDay));

						Console.WriteLine(av.StartDate);
						Console.WriteLine(av.EndDate);
						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsMo)
						{
							Console.WriteLine(ts.Datetime);
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Tuesday:
					if (model.TimeslotsTu.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsTu.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsTu.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsTu)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Wednesday:
					if (model.TimeslotsWe.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsWe.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsWe.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsWe)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Thursday:
					if (model.TimeslotsTh.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsTh.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsTh.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsTh)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Friday:
					if (model.TimeslotsFr.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsFr.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsFr.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsFr)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Saturday:
					if (model.TimeslotsSa.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsSa.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsSa.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsSa)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				case DayOfWeek.Sunday:
					if (model.TimeslotsSu.Count() > 0)
					{
						Availability av = new Availability(start.Date.Add(model.TimeslotsSu.First().Datetime.TimeOfDay),
							start.Date.Add(model.TimeslotsSu.Last().Datetime.TimeOfDay));

						employee.Availability(av);

						List<Timeslot> slots = new List<Timeslot>();
						foreach (var ts in model.TimeslotsSu)
						{
							slots.Add(
								new Timeslot(start.Date.Add(ts.Datetime.TimeOfDay), ts.Duration)
							);
						}

						employee.Timeslot(slots);
					}
					break;

				default:
					Console.WriteLine("Invalid day of the week");
					break;
			}
			start = start.AddDays(1);
		}

		await dbContext.SaveChangesAsync();
	}
}
