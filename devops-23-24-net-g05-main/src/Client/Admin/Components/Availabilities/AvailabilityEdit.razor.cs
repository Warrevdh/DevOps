using Microsoft.AspNetCore.Components;
using NPOI.SS.Formula.Functions;
using Radzen;
using Radzen.Blazor;
using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Employees;
using Shared.Users.Teams.Groups;

namespace Client.Admin.Components.Availabilities;

public partial class AvailabilityEdit
{
    [Inject] public IAvailabilityService AvailabilityService { get; set; } = default!;
    [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private bool loading = false;

    private EmployeeDto.Detail employee = default!;
    private string type = "Daily";
    private bool showTimeslots = false;
    private bool showTimeslotPopup = false;
    private int showTimeslotsWeek = -1;

    private DateTime? startDate;
    private DateTime? endDate;

    private IList<AvailabilityDto.Index> availabilities = default!;
    private IList<TimeslotDto.Mutate> timeslots = default!;
    private IList<IList<TimeslotDto.Mutate>> timeslotsWeek = default!;

    private TimeslotDto.Mutate activeTimeslot = default!;

    protected override async Task OnParametersSetAsync()
    {
        employee = await EmployeeService.GetDetailAsync(1);
        Daily();
    }

    private void Daily()
    {
        type = "Daily";
        availabilities = new List<AvailabilityDto.Index>(1)
        {
            new AvailabilityDto.Index()
        };
        timeslots = new List<TimeslotDto.Mutate>();
        timeslotsWeek = default!;
    }

    public void OnChangeStartTime(ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime result))
        {
            availabilities[0].StartDate = result;
            CheckTimeslotDaily();
        }
        else
        {
            Console.WriteLine("Invalid time format");
        }
    }

    public void OnChangeEndTime(ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime result))
        {
            availabilities[0].EndDate = result;
            CheckTimeslotDaily();
        }
        else
        {
            Console.WriteLine("Invalid time format");
        }
    }

    private void CheckTimeslotDaily()
    {
        if (availabilities[0].StartDate != default(DateTime) && availabilities[0].EndDate != default(DateTime))
        {
            timeslots.Clear();
            var start = availabilities[0].StartDate;
            int loop = 0;

            while(start <= availabilities[0].EndDate)
            {
                Console.WriteLine(start);
                Console.WriteLine(availabilities[0].EndDate);
                timeslots.Add(new TimeslotDto.Mutate
                {
                    Datetime = start,
                    Duration = new TimeSpan(0, 15, 0),
                });

                loop++;
                
                if (loop == 3)
                {
                    Console.WriteLine("ADDING BREAK");
                    start = start.AddMinutes(45);
                    loop = 0;
                } else
                {
                    Console.WriteLine("ADDING 15");
                    start = start.AddMinutes(15);
                
                }
            }

            showTimeslots = true;
        }
    }

	public void TogglePopup(TimeslotDto.Mutate ts)
    {
        activeTimeslot = ts;
        showTimeslotPopup = true;
        
    }

    public void ClosePopup()
    {
        showTimeslotPopup = false;
        activeTimeslot = default!;
    }

    private void Weekly()
	{
		type = "Weekly";
        availabilities = new List<AvailabilityDto.Index>(7);
        for (int i = 0; i < 7; i++)
        {
            availabilities.Add(new AvailabilityDto.Index());
        }

        timeslots = default!;
        timeslotsWeek = new List<IList<TimeslotDto.Mutate>>();

        for (int i = 0; i < 7; i++)
        {
            timeslotsWeek.Add(new List<TimeslotDto.Mutate>());
        }
    }

    public void OnChangeStartTimeW(int weekDay, ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime result))
        {
            availabilities[weekDay].StartDate = result;
            CheckTimeslotWeekly(weekDay);
        }
        else
        {
            Console.WriteLine("Invalid time format");
        }
    }

    public void OnChangeEndTimeW(int weekDay, ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime result))
        {
            availabilities[weekDay].EndDate = result;
            CheckTimeslotWeekly(weekDay);
        }
        else
        {
            Console.WriteLine("Invalid time format");
        }
    }

    private void CheckTimeslotWeekly(int weekDay)
    {
        if (availabilities[weekDay].StartDate != default(DateTime) && availabilities[weekDay].EndDate != default(DateTime)) ////TEST EMPTY
        {
            timeslotsWeek[weekDay].Clear();
            var start = availabilities[weekDay].StartDate;
            int loop = 0;

            while (start <= availabilities[weekDay].EndDate)
            {
                Console.WriteLine(start);
                Console.WriteLine(availabilities[weekDay].EndDate);
                timeslotsWeek[weekDay].Add(new TimeslotDto.Mutate
                {
                    Datetime = start,
                    Duration = new TimeSpan(0, 15, 0),
                });

                loop++;

                if (loop == 3)
                {
                    Console.WriteLine("ADDING BREAK");
                    start = start.AddMinutes(45);
                    loop = 0;
                }
                else
                {
                    Console.WriteLine("ADDING 15");
                    start = start.AddMinutes(15);

                }
            }
            ShowTimeslotsWeekday(weekDay);
        }
    }

    public void ShowTimeslotsWeekday(int weekday)
    {
        if (timeslotsWeek[weekday].Count > 0)
        {
            showTimeslotsWeek = weekday;
        }
        
    }

    public async void SaveDaily()
    {
        if(availabilities[0].StartDate < availabilities[0].EndDate && startDate <= endDate )
        {
            loading = true;
            await EmployeeService.AddDailyAvailabilitiesAsync(employee.Id, new EmployeeDto.MutateAvailabilityDaily
            {
                Availability = new AvailabilityDto.Mutate
                {
                    StartDate = startDate.Value.Date.Add(availabilities[0].StartDate.TimeOfDay),
                    EndDate = endDate.Value.Date.Add(availabilities[0].EndDate.TimeOfDay),
                    Employee = employee,
                },
                Timeslots = timeslots,
            });
            loading = false;
            NavigationManager.NavigateTo("/admin");
        } else
        {
            //POPUP ERROR
            Console.WriteLine("ERROR");
        }
        
    }

    public async void SaveWeekly()
    {
        //if (availabilities[0].StartDate < availabilities[0].EndDate && startDate <= endDate)
        if (startDate <= endDate)
        {
            loading= true;

            EmployeeDto.MutateAvailabilityWeekly weekly = new();
            weekly.Availability = new AvailabilityDto.Mutate
            {
                StartDate = startDate.Value.Date.Add(availabilities[0].StartDate.TimeOfDay),
                EndDate = endDate.Value.Date.Add(availabilities[0].EndDate.TimeOfDay),
                Employee = employee,
            };

			weekly.TimeslotsMo = timeslotsWeek[0];
			weekly.TimeslotsTu = timeslotsWeek[1];
			weekly.TimeslotsWe = timeslotsWeek[2];
			weekly.TimeslotsTh = timeslotsWeek[3];
			weekly.TimeslotsFr = timeslotsWeek[4];
			weekly.TimeslotsSa = timeslotsWeek[5];
			weekly.TimeslotsSu = timeslotsWeek[6];

            await EmployeeService.AddWeeklyAvailabilitiesAsync(employee.Id, weekly);
            loading= false;
            NavigationManager.NavigateTo("/admin");
		} else
        {
            //POPUP ERROR
            Console.WriteLine("ERROR");
        }
    }
}
