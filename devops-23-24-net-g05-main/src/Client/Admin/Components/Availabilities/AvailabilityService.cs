using Client.Classes;
using Client.Extensions;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using System.Net.Http.Json;

namespace Client.Admin.Components.Availabilities;

public class AvailabilityService : IAvailabilityService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
    private const string endpoint = "api/Availability";

    public AvailabilityService(HttpClient authenticatedClient, PublicClient publicClient)
    {
        this.authorizedClient = authenticatedClient;
        this.publicClient = publicClient;
    }

    public Task<AvailabilityResult.Create> CreateAsync(AvailabilityDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public async Task<AvailabilityResult.Index> GetAvailibilitiesFromEmployeeAsync(AvailabilityRequest.Index request, long employeeId)
    {
        var response = await publicClient.Client.GetFromJsonAsync<AvailabilityResult.Index>($"{endpoint}/Employee/{employeeId}?{request.AsQueryString()}");
        return response!;
    }

    public Task<AvailabilityResult.Index> GetIndexAsync(AvailabilityRequest.Index request)
    {
        throw new NotImplementedException();
    }
}
