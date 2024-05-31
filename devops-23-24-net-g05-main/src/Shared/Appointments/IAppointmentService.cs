namespace Shared.Appointments;
public interface IAppointmentService
{
	Task<AppointmentResult.Index> GetIndexAsync(AppointmentRequest.Index request);
	Task<AppointmentResult.Index> GetAppointmentsFromDoctor(AppointmentRequest.Index request, long doctorId);
	Task<AppointmentDto.Detail> GetDetailAsync(long appointmentId);
    Task<long> CreateAsync(AppointmentDto.Mutate model);
	Task<AppointmentResult.UltraDetail> GetAllDetailAppointmentsAsync(long doctorId);
    //Task EditAsync(long appointmentId, AppointmentDto.Mutate model);
    //Task DeleteAsync(long appointmentId);
}