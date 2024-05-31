namespace Shared.Users.Team.Doctors;

public interface IDoctorService
{
	Task<DoctorResult.Index> GetIndexAsync(DoctorRequest.Index request);
	Task<DoctorDto.Detail> GetDetailAsync(long doctorId);
	Task<DoctorDto.Detail> GetByEmailAsync(string email);
}
