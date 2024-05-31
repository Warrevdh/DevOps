using Client.Classes;
using Client.Extensions;
using Shared.EyeConditions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.EyeConditions;

public class EyeConditionService : IEyeConditionService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authenticatedClient;
    private const string endpoint = "api/EyeCondition";
	public EyeConditionService(HttpClient authenticatedClient, PublicClient publicClient)
	{
		this.authenticatedClient = authenticatedClient;
        this.publicClient = publicClient;
	}
	
	public async Task<EyeConditionResult.Index> GetIndexAsync(EyeConditionRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EyeConditionResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
	}

    public async Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate model)
    {
        var response = await authenticatedClient.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<EyeConditionResult.Create>();
    }

    public async Task EditAsync(long id, EyeConditionDto.Mutate edit)
    {
	   var response = await authenticatedClient.PutAsJsonAsync($"{endpoint}/{id}", edit);
 	   response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(long id)
    {
        await authenticatedClient.DeleteAsync($"{endpoint}/{id}");
    }

    //public async Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate request)
    //{
    //	var response = await client.PostAsJsonAsync(endpoint, request);
    //	return await response.Content.ReadFromJsonAsync<EyeConditionResult.Create>();
    //}

    //public async Task DeleteAsync(int eyeConditionId)
    //{
    //	await client.DeleteAsync($"{endpoint}/{eyeConditionId}");
    //}

    public async Task<EyeConditionDto.Detail> GetDetailAsync(long eyeConditionId)
    {
        var response = await publicClient.Client.GetFromJsonAsync<EyeConditionDto.Detail>($"{endpoint}/{eyeConditionId}");
        return response;
    }

    public async Task<EyeConditionDto.Mutate> GetMutateAsync(long eyeConditionId)
    {
        var response = await authenticatedClient.GetFromJsonAsync<EyeConditionDto.Mutate>($"{endpoint}/{eyeConditionId}");
        return response;
    }


    //public async Task EditAsync(int eyeConditionId, EyeConditionDto.Mutate model)
    //{
    //	var response = await client.PutAsJsonAsync($"{endpoint}/{eyeConditionId}", model);
    //}
}