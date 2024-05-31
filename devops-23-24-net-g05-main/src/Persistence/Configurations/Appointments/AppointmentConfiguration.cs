using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Appointments;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.Appointments;
internal class AppointmentConfiguration : EntityConfiguration<Appointment>
{
	public override void Configure(EntityTypeBuilder<Appointment> builder)
	{
		base.Configure(builder);

		builder.HasOne(x => x.Patient);

		builder.HasOne(x => x.Timeslot);

		builder.Property(x => x.Reason);
		builder.Property(x => x.Note);
	}
}
