namespace Shared.Users.Doctors.Availabilities;
public interface IAvailabilityService
{

	Task<AvailabilityResult.Index> GetIndexAsync(AvailabilityRequest.Index request);
	Task<AvailabilityResult.Index> GetAvailibilitiesFromEmployeeAsync(AvailabilityRequest.Index request, long employeeId);
    Task<AvailabilityResult.Create> CreateAsync(AvailabilityDto.Mutate model);
}
