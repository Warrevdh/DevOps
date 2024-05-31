using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Domain.Users.Doctors;
using Persistence;
using Shared.Users.Teams.Biographies;

namespace Services.Users.Team;

public class BioService : IBioService
{
	private readonly ApplicationDbContext dbContext;

	public BioService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<BioDto.Index> GetDetailAsync(long bioId)
	{
		BioDto.Index? bio = await dbContext.Biographies.Select(x => new BioDto.Index
		{
			Id = x.Id,
			Info = x.Info,
		}).SingleOrDefaultAsync(x => x.Id == bioId);

		if (bio is null)
		{
			throw new EntityNotFoundException(nameof(bio), bioId);
		}

		return bio;
	}

	public async Task<long> CreateAsync(BioDto.Mutate model)
	{
		Bio bio = new(model.Info!);

		dbContext.Biographies.Add(bio);
		await dbContext.SaveChangesAsync();

		return bio.Id;
	}

	public async Task EditAsync(long bioId, BioDto.Mutate model)
	{
		Bio? bio = await dbContext.Biographies.SingleOrDefaultAsync(x => x.Id == bioId);

		if (bio is null)
		{
			throw new EntityNotFoundException(nameof(bio), bioId);
		}

		bio.Info = model.Info!;

		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(long bioId)
	{
		Bio? bio = await dbContext.Biographies.SingleOrDefaultAsync(x => x.Id == bioId);

		if (bio is null)
		{
			throw new EntityNotFoundException(nameof(bio), bioId);
		}

		dbContext.Biographies.Remove(bio);

		await dbContext.SaveChangesAsync();
	}
}
