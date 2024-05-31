using Shared.Users.Teams.Groups;

namespace Shared.Appointments.Timeslots;

public interface ITimeslotService
{
	Task<TimeslotResult.Index> GetIndexAsync(TimeslotRequest.Index request);
	Task<TimeslotResult.Index> GetTimeslotsFromDoctorAsync(TimeslotRequest.Index request, long doctorId);
    Task<long> CreateAsync(TimeslotDto.Mutate model);
    Task DeleteAsync(long timeslotId);
    Task EditAsync(long timeslotId, TimeslotDto.Mutate model);
}
