using Shared.Appointments.Timeslots;
using Shared.Users.Doctors.Availabilities;
using Shared.Users.Doctors.Specializations;
using Shared.Users.Teams.Groups;

namespace Shared.Users.Team.Doctors;

public abstract class DoctorDto
{
	public class Index
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public IEnumerable<SpecializationDto.Index>? Specializations { get; set; }
		public GroupDto.Index? Group { get; set; }
	}

	public class Detail
	{
		public long Id { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public DateTime Birthdate { get; set; }
		public string? Email { get; set; }
		public string? Phonenumber { get; set; }
		public GroupDto.Index? Group { get; set; }
		public IEnumerable<AvailabilityDto.Index>? Availabilities { get; set; }
		public IEnumerable<SpecializationDto.Index>? Specializations { get; set; }
		public IEnumerable<TimeslotDto.Index>? Timeslots { get; set; }
	}
}
