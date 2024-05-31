using Client.Classes;
using Client.Files;
using Shared.Users.Teams.Biographies;
using System.Net.Http.Json;

namespace Client.Team;

public class BioService : IBioService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
	private const string endpoint = "api/Bio";

	public BioService(HttpClient authorizedClient, PublicClient publicClient)
	{
		this.authorizedClient = authorizedClient;
		this.publicClient = publicClient;
	}

	public Task<long> CreateAsync(BioDto.Mutate model)
	{
		throw new NotImplementedException();
	}

	public Task DeleteAsync(long bioId)
	{
		throw new NotImplementedException();
	}

	public Task EditAsync(long bioId, BioDto.Mutate model)
	{
		throw new NotImplementedException();
	}

	public async Task<BioDto.Index> GetDetailAsync(long bioId)
	{
		var response = await publicClient.Client.GetFromJsonAsync<BioDto.Index>($"{endpoint}/{bioId}");
		return response;
	}
}
