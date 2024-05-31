using Client.Classes;
using Client.Extensions;
using Shared.Users.Doctors.Employees;
using System.Net.Http.Json;

namespace Client.Team;

public class EmployeeService : IEmployeeService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
    private const string endpoint = "api/Employee";

	public EmployeeService(HttpClient authorizedClient, PublicClient publicClient)
	{
		this.authorizedClient = authorizedClient;
		this.publicClient = publicClient;
	}
	
	public async Task<EmployeeResult.Index> GetIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}?{request.AsQueryString()}");
		return response;
	}
	
	public async Task<EmployeeResult.Index> GetDoctorsIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Doctors?{request.AsQueryString()}");
		return response;
	}
	public async Task<EmployeeResult.Index> GetAssistantsIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Assistants?{request.AsQueryString()}");
		return response;
	}
	
	public async Task<EmployeeResult.Index> GetSecretaryIndexAsync(EmployeeRequest.Index request)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeResult.Index>($"{endpoint}/Secretary?{request.AsQueryString()}");
		return response;
	}

	public async Task<EmployeeDto.Detail> GetDetailAsync(long employeeId)
	{
		var response = await publicClient.Client.GetFromJsonAsync<EmployeeDto.Detail>($"{endpoint}/{employeeId}");
		return response;
	}

	public async Task EditAsync(long employeeId, EmployeeDto.Mutate model)
	{
		var response = await authorizedClient.PutAsJsonAsync($"{endpoint}/{employeeId}", model);
        response.EnsureSuccessStatusCode();
    }

    public async Task<EmployeeResult.Create> CreateAsync(EmployeeDto.Mutate model)
    {
        var response = await authorizedClient.PostAsJsonAsync(endpoint, model);
        return await response.Content.ReadFromJsonAsync<EmployeeResult.Create>();
    }

    public async Task DeleteAsync(long id)
    {
        await authorizedClient.DeleteAsync($"{endpoint}/{id}");
    }

    public async Task<EmployeeDto.Mutate> GetMutateAsync(long employeeId)
    {
        var response = await authorizedClient.GetFromJsonAsync<EmployeeDto.Mutate>($"{endpoint}/{employeeId}");
		return response;
    }
    
	public async Task ChangeGroupAsync(long employeeId, long groupId)
	{
		var response = await authorizedClient.PutAsync($"{endpoint}/{employeeId}/{groupId}", null);
	}

    public async Task AddDailyAvailabilitiesAsync(long employeeId, EmployeeDto.MutateAvailabilityDaily model)
    {
        var response = await authorizedClient.PutAsJsonAsync($"{endpoint}/{employeeId}/Availability/Daily", model);
    }

    public async Task AddWeeklyAvailabilitiesAsync(long employeeId, EmployeeDto.MutateAvailabilityWeekly model)
    {
		var response = await authorizedClient.PutAsJsonAsync($"{endpoint}/{employeeId}/Availability/Weekly", model);
	}
}