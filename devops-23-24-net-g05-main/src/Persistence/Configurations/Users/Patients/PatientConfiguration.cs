using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Users.Patients;

namespace Persistence.Configurations.Users.Patients;

internal class PatientConfiguration : EntityConfiguration<Patient>
{
	public override void Configure(EntityTypeBuilder<Patient> builder)
	{
		base.Configure(builder);

		builder.Property(ec => ec.FirstName);

		builder.Property(ec => ec.LastName);

		builder.Property(ec => ec.BirthDate);

		builder.Property(ec => ec.PhoneNumber);

		builder.Property(ec => ec.Email);
	}
}
