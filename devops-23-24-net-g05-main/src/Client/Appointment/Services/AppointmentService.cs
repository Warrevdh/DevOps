using Client.Classes;
using Client.Extensions;
using Shared.Appointments;
using Shared.EyeConditions;
using Shared.Users.Team.Doctors;
using System.Net.Http.Json;

namespace Client.Appointment.Services;

public class AppointmentService : IAppointmentService
{
    private readonly PublicClient publicClient;
    private readonly HttpClient authenticatedClient;
    private const string endpoint = "api/Appointment";

    public AppointmentService(HttpClient authenticatedClient, PublicClient publicClient)
    {
        this.authenticatedClient = authenticatedClient;
        this.publicClient = publicClient;
    }
    public async Task<long> CreateAsync(AppointmentDto.Mutate model)
    {
        var response = await publicClient.Client.PostAsJsonAsync(endpoint, model);
        Console.WriteLine(response.Content);
        return await response.Content.ReadFromJsonAsync<long>();
    }

    public Task<AppointmentResult.Index> GetAppointmentsFromDoctor(AppointmentRequest.Index request, long doctorId)
    {
		throw new NotImplementedException();
	}

    public Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request)
    {
        throw new NotImplementedException();
    }

	public async Task<AppointmentResult.UltraDetail> GetAllDetailAppointmentsAsync(long doctorId)
	{
		var response = await authenticatedClient.GetFromJsonAsync<AppointmentResult.UltraDetail>($"{endpoint}/Doctor/Detail/{doctorId}");
		return response!;
	}
}
