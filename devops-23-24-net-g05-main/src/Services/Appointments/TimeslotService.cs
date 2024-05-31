using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Appointments.Timeslots;
using Domain.Appointments;

namespace Services.Appointments;

public class TimeslotService : ITimeslotService
{
	private readonly ApplicationDbContext dbContext;

	public TimeslotService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

    public async Task<TimeslotResult.Index> GetIndexAsync(TimeslotRequest.Index request)
	{
		var parsedDate = request.Date != null ? DateTime.Parse(request.Date) : (DateTime?)null;
		var query = dbContext.Timeslots.AsQueryable();

		var items = await query
			.Where(x => parsedDate == null || parsedDate == x.Time.Date)    //If date from request is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Datetime = x.Time,
				Duration = x.Duration,
			})
			.ToListAsync();

		var result = new TimeslotResult.Index
		{
			Timeslots = items,
			TotalAmount = items.Count,
		};

		return result;
	}

	public async Task<TimeslotResult.Index> GetTimeslotsFromDoctorAsync(TimeslotRequest.Index request, long doctorId)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();
        var parsedDate = request.Date != null ? DateTime.Parse(request.Date) : (DateTime?)null;

        var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Timeslots)
			.Where(x => parsedDate == null || parsedDate == x.Time.Date)   //If date is null, this where clause doesn't do anything
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new TimeslotDto.Index
			{
				Id = x.Id,
				Datetime = x.Time,
				Duration = x.Duration,
			})
			.ToListAsync();

		var result = new TimeslotResult.Index
		{
			Timeslots = items,
			TotalAmount = items.Count,
		};

		return result;
	}

    public async Task<long> CreateAsync(TimeslotDto.Mutate model)
    {
        Timeslot timeslot = new(model.Datetime!, model.Duration!);

		dbContext.Timeslots.Add(timeslot);
		await dbContext.SaveChangesAsync();

		return timeslot.Id;
    }

    public async Task DeleteAsync(long timeslotId)
    {
        Timeslot? ts = await dbContext.Timeslots.SingleOrDefaultAsync(x => x.Id ==  timeslotId);

		if (ts is null)
		{
			throw new EntityNotFoundException(nameof(ts), timeslotId);
		}

		dbContext.Timeslots.Remove(ts);
		await dbContext.SaveChangesAsync();
    }

    public async Task EditAsync(long timeslotId, TimeslotDto.Mutate model)
    {
		Timeslot? ts = await dbContext.Timeslots.SingleOrDefaultAsync(x => x.Id == timeslotId);

		if (ts is null)
		{
            throw new EntityNotFoundException(nameof(Timeslot), timeslotId);
        }

		ts.Time = model.Datetime!;
		ts.Duration = model.Duration!;

		await dbContext.SaveChangesAsync();
    }
}
