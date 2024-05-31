using Microsoft.AspNetCore.Components;
using Radzen;
using Shared.Appointments;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Employees;
using Shared.Users.Team.Doctors;

namespace Client.Appointment;

public partial class Index
{
    [Parameter] public DateTime DateValue { get; set; } = DateTime.Now;
    [Parameter] public int ActiveCount { get; set; } = 1;
    [Parameter] public int SelectCount { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public IDoctorService DoctorService { get; set; } = default!;
    [Inject] public IAvailabilityService AvailabilityService { get; set; } = default!;
    [Inject] public ITimeslotService TimeslotService { get; set; } = default!;

    private bool loading = false;

    private IEnumerable<DoctorDto.Index> doctors = default!;
    private IEnumerable<TimeslotDto.Index> timeslots = new List<TimeslotDto.Index>();
    private IEnumerable<AvailabilityDto.Index> availabilities = default!;

    private long value = -1;
    private DateTime selectedDate;

    private AppointmentDto.Mutate newAppointment = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var doctorResp = await DoctorService.GetIndexAsync(new DoctorRequest.Index { });
        newAppointment = new AppointmentDto.Mutate();
        selectedDate = DateTime.Today;
        doctors = doctorResp.Doctors!;
    }

    private void DateRenderSpecial(DateRenderEventArgs args)
    {
        if (args.Date < DateTime.Today)
        {
            args.Disabled = true;
            args.Attributes.Add("style", "color: #d3d3d3;");
        }
    }

    private async void OnChangeDr(object value)
    {
        try
        {
            long result = Convert.ToInt64(value);
            newAppointment.Employee = new EmployeeDto.Index { Id = result};
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Conversion to long failed.");
        }

        
        await LoadData();
        StateHasChanged();
    }

    private async void OnChangeDate(DateTime? value)
    {
        if(value is not null)
        {
            Console.WriteLine($"Value changed to {value}");
            selectedDate = (DateTime)value;
            Console.WriteLine("LOADING");
            await LoadData();
            StateHasChanged();
        }
    }

    private async Task LoadData()
    {
        loading = true;
        var avResp = await AvailabilityService.GetAvailibilitiesFromEmployeeAsync(
            new AvailabilityRequest.Index { },
            (long)value);

        var ts = await TimeslotService.GetTimeslotsFromDoctorAsync(
            new TimeslotRequest.Index
            {
                Date = selectedDate.ToString("yyyy-MM-dd")
            },
            (long)value);

        availabilities = avResp.Availabilities!;
        timeslots = ts.Timeslots!;
        Console.WriteLine("FIXED");
        loading = false;
    }

    public void Reset()
    {
        NavigationManager.NavigateTo($"Afspraak", true);
    }

    public void Next()
    {
        if(newAppointment.Employee is not null && newAppointment.Timeslot is not null)
        {
            ActiveCount++;
        }
    }

    private void SelectTimeslot(TimeslotDto.Index ts)
    {
        newAppointment.Timeslot = ts;
    }
}
