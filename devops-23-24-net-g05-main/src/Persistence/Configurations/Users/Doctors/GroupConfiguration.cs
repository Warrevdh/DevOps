using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Configurations;

namespace Persistence.Configurations.Users.Doctors;

internal class GroupConfiguration : EntityConfiguration<Group>
{
	public override void Configure(EntityTypeBuilder<Group> builder)
	{
		base.Configure(builder);

		builder.Property(x => x.Name).IsRequired();
	}
}
