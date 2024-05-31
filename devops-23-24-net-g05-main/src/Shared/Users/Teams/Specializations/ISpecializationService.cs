namespace Shared.Users.Doctors.Specializations;
public interface ISpecializationService
{
	Task<SpecializationResult.Index> GetIndexAsync(SpecializationRequest.Index request);
	Task<SpecializationResult.Index> GetSpecializationsFromDoctorAsync(long doctorId);
}
