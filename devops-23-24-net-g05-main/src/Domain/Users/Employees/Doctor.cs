using Domain.Users.Employees;
using Domain.Users.Employees.Availabilities;
using Domain.Appointments;

namespace Domain.Users.Doctors;
public class Doctor : Employee
{
	private readonly List<Specialization> specializations = new();
	public IReadOnlyCollection<Specialization> Specializations => specializations.AsReadOnly();

	private readonly List<Timeslot> timeslots = new();
	public IReadOnlyCollection<Timeslot> Timeslots => timeslots.AsReadOnly();

	private readonly List<Appointment> appointments = new();
	public IReadOnlyCollection<Appointment> Appointments => appointments.AsReadOnly();

	//Database constructor
	private Doctor() { }

	public Doctor(string firstname, string lastname, DateTime birthdate, string phonenumber, string email, Group group, string image)
	{
		FirstName = firstname;
		LastName = lastname;
		Image = image;
		PhoneNumber = phonenumber;
		Email = email;
		Birthdate = birthdate;
		Group = group;
	}

	public void Specialization(Specialization specialization)
	{
		Guard.Against.Null(specialization, nameof(specialization));

		if (specializations.Contains(specialization))
		{
			throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' already has this specialization");
		}
		specializations.Add(specialization);
	}

	public void RemoveSpecialization(Specialization specialization)
	{
		Guard.Against.Null(specialization, nameof(specialization));

		if (!specializations.Contains(specialization))
		{
			throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' does not have this specialization");
		}
		specializations.Remove(specialization);
	}

	public void Timeslot(List<Timeslot> ts)
	{
		Guard.Against.NullOrEmpty(ts, nameof(ts));

		List<Availability> avs = Availabilities.ToList();

		foreach (Timeslot timeslot in ts)
		{
			if (timeslots.Contains(timeslot))
			{
				throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' already has one or more of these timeslots");
			} else
			{
				//Verify if doctor is available during timeslot
				var a = avs.Find(x => x.StartDate.Date == timeslot.Time.Date);
				//var a = avs.Find(x => x.StartDate.Date.ToString().Equals(timeslot.Time.ToString("dd/M/yyyy")));

				if (a is null || a.StartDate.TimeOfDay > timeslot.Time.TimeOfDay || a.EndDate.TimeOfDay < timeslot.Time.TimeOfDay)
				{
					/*throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' is not available during one or more of these timeslots");*/
				}
			}
		}

		foreach (Timeslot timeslot in ts)
		{
			timeslots.Add(timeslot);
		}
	}

	public void RemoveTimeslot(Timeslot timeslot)
	{
		Guard.Against.Null(timeslot, nameof(timeslot));

		if (!timeslots.Contains(timeslot))
		{
			throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' does not have this timeslot");
		}
		timeslots.Remove(timeslot);
	}

	public void Appointment(Appointment appointment)
	{
		Guard.Against.Null(appointment, nameof(appointment));

		if(!timeslots.Contains(appointment.Timeslot))
		{
			throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' does not have a corresponding timeslot for this appointment");
		}

		if (appointments.Contains(appointment))
		{
			throw new ApplicationException($"{nameof(Doctor)} '{FirstName}' '{LastName}' already has this appointment");
		}
		appointments.Add(appointment);
	}

	public void RemoveAppointment(Appointment appointment)
	{
		Guard.Against.Null(appointment, nameof(appointment));

		if (!appointments.Contains(appointment))
		{
			throw new ApplicationException($"{nameof(Appointment)} '{FirstName}' '{LastName}' does not have this appointment");
		}
		appointments.Remove(appointment);
	}
}

