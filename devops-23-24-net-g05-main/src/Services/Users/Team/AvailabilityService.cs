using Domain.Users.Doctors;
using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Users.Doctors.Availabilities;

namespace Services.Users.Doctors;
public class AvailabilityService : IAvailabilityService
{
	private readonly ApplicationDbContext dbContext;

	public AvailabilityService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

    public async Task<AvailabilityResult.Index> GetIndexAsync(AvailabilityRequest.Index request)
	{
		var query = dbContext.Availabilities.AsQueryable();

		var items = await query
			.Where(x => request.Date == default(DateTime) || x.StartDate.Date == request.Date)  //If no date, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new AvailabilityDto.Index
			{
				Id = x.Id,
				StartDate = x.StartDate,
				EndDate = x.EndDate,
			})
			.ToListAsync();

		var result = new AvailabilityResult.Index
		{
			Availabilities = items,
			TotalAmount = items.Count,
		};

		return result;
	}

    public async Task<AvailabilityResult.Index> GetAvailibilitiesFromEmployeeAsync(AvailabilityRequest.Index request, long employeeId)
    {
        var query = dbContext.Employees.AsQueryable();

        var items = await query
            .Where(x => x.Id == employeeId)
            .SelectMany(x => x.Availabilities)
            .Where(x => request.Date == default(DateTime) || x.StartDate.Date == request.Date)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderBy(x => x.Id)
            .Select(x => new AvailabilityDto.Index
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            })
            .ToListAsync();

        var result = new AvailabilityResult.Index
        {
            Availabilities = items,
            TotalAmount = items.Count,
        };

        return result;
    }

    public async Task<AvailabilityResult.Create> CreateAsync(AvailabilityDto.Mutate model)
    {
        Employee? employee = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == model.Employee.Id);

        if (employee is null)
        {
            throw new EntityNotFoundException(nameof(employee), model.Employee.Id);
        }

        Availability availability = new(model.StartDate, model.EndDate);
        employee.Availability(availability);

        dbContext.Availabilities.Add(availability);
        await dbContext.SaveChangesAsync();


        AvailabilityResult.Create result = new()
        {
            Id = availability.Id,
            StartDate = availability.StartDate,
            EndDate = availability.EndDate,
        };

        return result;
    }

}
