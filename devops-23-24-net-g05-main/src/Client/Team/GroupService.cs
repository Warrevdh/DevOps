using Client.Classes;
using Client.Extensions;
using Shared.Users.Teams.Groups;
using System.Net.Http.Json;

namespace Client.Team;

public class GroupService : IGroupService
{
	private readonly HttpClient authorizedClient;
    private readonly PublicClient publicClient;
    private const string endpoint = "api/Group";

	public GroupService(HttpClient authorizedClient, PublicClient publicClient)
	{
		this.authorizedClient = authorizedClient;
		this.publicClient = publicClient;
	}

	public async Task<GroupResult.Index> GetIndexAsync(GroupRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<GroupResult.Index>($"{endpoint}?{request.AsQueryString()}");
		return response!;
	}

	public async Task<long> CreateAsync(GroupDto.Mutate model)
	{
		var response = await authorizedClient.PostAsJsonAsync(endpoint, model);
		return await response.Content.ReadFromJsonAsync<long>();
	}

	public async Task DeleteAsync(long groupId)
	{
		await authorizedClient.DeleteAsync($"{endpoint}/{groupId}");
	}

    public async Task EditAsync(long groupId, GroupDto.Mutate model)
    {
		var response = await authorizedClient.PutAsJsonAsync($"{endpoint}/{groupId}", model);
    }
}
