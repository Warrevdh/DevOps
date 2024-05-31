using Client.Classes;
using Client.Extensions;
using Shared.EyeConditions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Symptoms;

public class SymptomService : ISymptomService
{
    private readonly HttpClient authorizedClient;
    private readonly PublicClient publicClient;
    private const string endpoint = "api/symptom";

    public SymptomService(HttpClient authorizedClient, PublicClient publicClient)
    {
        this.authorizedClient = authorizedClient;
        this.publicClient = publicClient;
    }

    public async Task<SymptomResult.Index> GetIndexAsync(SymptomRequest.Index request)
    {
        var response = await publicClient.Client.GetFromJsonAsync<SymptomResult.Index>($"{endpoint}?{request.AsQueryString()}");
        return response!;
    }

    public async Task<SymptomResult.Create> CreateAsync(SymptomDto.Mutate model)
    {
        var response = await authorizedClient.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<SymptomResult.Create>();
    }

    public async Task DeleteAsync(long id)
    {
        await authorizedClient.DeleteAsync($"{endpoint}/{id}");
    }

    public async Task EditAsync(long symptomId, SymptomDto.Mutate model)
    {
        var response = await authorizedClient.PutAsJsonAsync($"{endpoint}/{symptomId}", model);
        response.EnsureSuccessStatusCode();
    }
}