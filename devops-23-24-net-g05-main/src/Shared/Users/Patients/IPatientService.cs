namespace Shared.Users.Patients;
public interface IPatientService
{
	Task<PatientResult.Index> GetIndexAsync(PatientRequest.Index request);
	Task<PatientDto.Detail> GetDetailAsync(long patientId);
	//Task<PatientResult.Create> CreateAsync(PatientDto.Mutate model);
	//Task<EyeConditionDto.Index> GetDetailAsync(int productId);

}
