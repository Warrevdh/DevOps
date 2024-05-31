using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Appointments;
using Shared.Infrastructure;
using Shared.Users.Teams.Groups;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Appointments;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Administrator")]
public class AppointmentController : ControllerBase
{
    //private readonly IAppointmentService appointmentService;
    //private readonly IEmailSender _emailSender;

	private readonly IAppointmentService appointmentService;

	public AppointmentController(IAppointmentService appointmentService)
	{
		this.appointmentService = appointmentService;
	}

	[SwaggerOperation("Returns a list of appointments.")]
	[HttpGet, AllowAnonymous]
	public async Task<AppointmentResult.Index> GetIndex([FromQuery] AppointmentRequest.Index request)
	{
		return await appointmentService.GetIndexAsync(request);
	}

	[SwaggerOperation("Returns a list of appointments for a specific doctor.")]
	[HttpGet("Doctor/{doctorId}"), AllowAnonymous]
	public async Task<AppointmentResult.Index> GetAppointmentsFromDoctor([FromQuery] AppointmentRequest.Index request, long doctorId)
	{
		return await appointmentService.GetAppointmentsFromDoctor(request, doctorId);
	}

	[SwaggerOperation("Returns a specific appointment.")]
	[HttpGet("{appointmentId}"), AllowAnonymous]
	public async Task<AppointmentDto.Detail> GetDetail(long appointmentId)
	{
		return await appointmentService.GetDetailAsync(appointmentId);
	}

    [SwaggerOperation("Creates a new appointment.")]
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Create(AppointmentDto.Mutate model)
    {
        var appointmentId = await appointmentService.CreateAsync(model);
        return CreatedAtAction(nameof(Create), appointmentId);
    }

	[SwaggerOperation("Returns a list of detailed appointments for a specific doctor.")]
	[HttpGet("Doctor/Detail/{doctorId}"), AllowAnonymous]
	public async Task<AppointmentResult.UltraDetail> GetDetailedAppointmentsFromDoctor(long doctorId)
	{
		return await appointmentService.GetAllDetailAppointmentsAsync(doctorId);
	}

	//[SwaggerOperation("Edites an existing appointment.")]
	//[HttpPut("{appointmentId}")]
	//public async Task<IActionResult> Edit(long appointmentId, AppointmentDto.Mutate model)
	//{
	//    await appointmentService.EditAsync(appointmentId, model);
	//    return NoContent();
	//}

	//[SwaggerOperation("Deletes an existing appointment.")]
	//[HttpDelete("{appointmentId}")]
	//public async Task<IActionResult> Delete(long appointmentId)
	//{
	//    await appointmentService.DeleteAsync(appointmentId);
	//    return NoContent();
	//}

	//[HttpPost("SendEmail")]
	//public async Task<IActionResult> SendEmail()
	//{
	//    try
	//    {
	//        // Call the injected email service to send the email
	//        await _emailSender.SendEmailAsync("yani.degrande@gmail.com", "Appointment Confirmation", "Your appointment is confirmed.");
	//        return Ok("Email sent successfully!");
	//    }
	//    catch (Exception ex)
	//    {
	//        return BadRequest($"Failed to send email: {ex.Message}");
	//    }
	//}
}
