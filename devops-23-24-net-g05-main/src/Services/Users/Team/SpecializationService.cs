using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Users.Doctors.Specializations;

namespace Services.Users.Doctors;
public class SpecializationService : ISpecializationService
{
	private readonly ApplicationDbContext dbContext;
	public SpecializationService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<SpecializationResult.Index> GetIndexAsync(SpecializationRequest.Index request)
	{
		var query = dbContext.Specializations.AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.Searchterm))
		{
			query = query.Where(x => x.Name.Contains(request.Searchterm));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Id)
			.Select(x => new SpecializationDto.Index
			{
				Id = x.Id,
				Name = x.Name,
			})
			.ToListAsync();

		var result = new SpecializationResult.Index
		{
			Specializations = items,
			TotalAmount = totalAmount
		};

		return result;
	}

	public async Task<SpecializationResult.Index> GetSpecializationsFromDoctorAsync(long doctorId)
	{
		var query = dbContext.Employees.OfType<Doctor>().AsQueryable();

		var items = await query
			.Where(x => x.Id == doctorId)
			.SelectMany(x => x.Specializations)
			.Select(x => new SpecializationDto.Index
			{
				Id = x.Id,
				Name = x.Name
			})
			.ToListAsync();

		var result = new SpecializationResult.Index
		{
			Specializations = items,
			TotalAmount = items.Count
		};

		return result;
	}
}
