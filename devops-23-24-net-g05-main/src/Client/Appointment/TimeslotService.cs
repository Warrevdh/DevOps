using Client.Classes;
using Client.Extensions;
using Shared.Appointments.Timeslots;
using System.Net.Http.Json;

namespace Client.Appointment;

public class TimeslotService : ITimeslotService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authorizedClient;
    private const string endpoint = "api/Timeslot";

    public TimeslotService(HttpClient authenticatedClient, PublicClient publicClient)
    {
        this.publicClient = publicClient;
        this.authorizedClient = authenticatedClient;
    }

    public Task<long> CreateAsync(TimeslotDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long timeslotId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(long timeslotId, TimeslotDto.Mutate model)
    {
        throw new NotImplementedException();
    }

    public Task<TimeslotResult.Index> GetIndexAsync(TimeslotRequest.Index request)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeslotResult.Index> GetTimeslotsFromDoctorAsync(TimeslotRequest.Index request, long doctorId)
    {
        var response = await publicClient.Client.GetFromJsonAsync<TimeslotResult.Index>($"{endpoint}/{doctorId}?{request.AsQueryString()}");
        return response!;
    }
}
