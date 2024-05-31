namespace Shared.Users.Teams.Groups;

public interface IGroupService
{
	Task<GroupResult.Index> GetIndexAsync(GroupRequest.Index request);
	Task<long> CreateAsync(GroupDto.Mutate model);
	Task DeleteAsync(long groupId);
	Task EditAsync(long groupId, GroupDto.Mutate model);
}
