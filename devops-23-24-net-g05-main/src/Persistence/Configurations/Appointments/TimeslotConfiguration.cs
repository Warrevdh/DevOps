using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Appointments;

namespace Persistence.Configurations.Appointments;

internal class TimeslotConfiguration : EntityConfiguration<Timeslot>
{
	public override void Configure(EntityTypeBuilder<Timeslot> builder)
	{
		base.Configure(builder);

		builder.Property(x => x.Time);
		builder.Property(x => x.Duration);
	}
}
