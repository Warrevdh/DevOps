using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Appointments;

public class Timeslot : Entity
{
	private DateTime time = default!;
	public DateTime Time
	{
		get => time;
		set => time = Guard.Against.Null(value, nameof(time));
	}

	private TimeSpan duration = default!;
	public TimeSpan Duration
	{
		get => duration;
		set => duration = Guard.Against.Null(value,nameof(duration));
	}

	//Database constructor
	private Timeslot() { }

	public Timeslot(DateTime time, TimeSpan duration = default)
	{
		Time = time;
		Duration = (duration == default) ? TimeSpan.FromMinutes(15) : duration;
	}
}
