using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Users.Teams.Groups;

namespace Services.Users.Team;

public class GroupService : IGroupService
{

	private readonly ApplicationDbContext dbContext;

	public GroupService(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public async Task<GroupResult.Index> GetIndexAsync(GroupRequest.Index request)
	{
		var query = dbContext.Groups.AsQueryable();

		if (!string.IsNullOrWhiteSpace(request.Name))
		{
			query = query.Where(x => x.Name.Contains(request.Name));
		}

		int totalAmount = await query.CountAsync();

		var items = await query
			.Skip((request.Page - 1) * request.PageSize)
			.Take(request.PageSize)
			.OrderBy(x => x.Sequence)
			.Select(x => new GroupDto.Index
			{
				Id = x.Id,
				Name = x.Name,
				Sequence = x.Sequence,
			}).ToListAsync();

		var result = new GroupResult.Index
		{
			Groups = items,
			TotalAmount = totalAmount,
		};

		return result;
	}

	public async Task<long> CreateAsync(GroupDto.Mutate model)
	{
		Group group = new(model.Name!, model.Sequence!);

		dbContext.Groups.Add(group);
		await dbContext.SaveChangesAsync();

		return group.Id;
	}

	public async Task DeleteAsync(long groupId)
	{
		Group? group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);

		if(group is null)
		{
			throw new EntityNotFoundException(nameof(Group), groupId);
		}

		if (await dbContext.Employees.AnyAsync(x => x.Group.Id == groupId))
		{
			throw new ApplicationException($"There are still employees tied to this group. Asign them to another group first");
			
		}

		dbContext.Groups.Remove(group);

		await dbContext.SaveChangesAsync();
	}

    public async Task EditAsync(long groupId, GroupDto.Mutate model)
    {
        Group? group = await dbContext.Groups.SingleOrDefaultAsync(x => x.Id == groupId);

        if (group is null)
		{
			throw new EntityNotFoundException(nameof(Group), groupId);
		}

		group.Name = model.Name;
		group.Sequence = model.Sequence;

		await dbContext.SaveChangesAsync();
    }
}
