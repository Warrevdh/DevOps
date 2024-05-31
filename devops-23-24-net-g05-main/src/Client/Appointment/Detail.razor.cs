using Microsoft.AspNetCore.Components;
using Shared.Infrastructure;

namespace Client.Appointment;

public partial class Detail
{
    [Parameter] public bool getAppointmentsSucceeded { get; set; }
    [Parameter] public bool getAppointmentsFailed { get; set; }
    [Inject] public IEmailSender EmailSender { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task SendEmail(string email)
    {
        await EmailSender.SendEmailAsync(email, "Afspraak confirmatie", "Confirmeren van de afspraak email");
    }

    private async Task HandleValidSubmit()
    {
       await SendEmail("yani.degrande@gmail.com");
    }
    private AppointmentDetailsModel appointmentDetailsModel = new AppointmentDetailsModel();

    private class AppointmentDetailsModel
    {

        public string Email { get; set; }
    }
}
