using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Users.Doctors;

namespace Persistence.Configurations.Users.Doctors;
internal class SpecializationConfiguration : EntityConfiguration<Specialization>
{
	public override void Configure(EntityTypeBuilder<Specialization> builder)
	{
		base.Configure(builder);

		builder.Property(ec => ec.Name).IsRequired();
	}
}

