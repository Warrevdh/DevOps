using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Users.Doctors;
using Domain.Users.Employees;

namespace Persistence.Configurations.Users.Doctors;

internal class EmployeeConfiguration : EntityConfiguration<Employee>
{
	public override void Configure(EntityTypeBuilder<Employee> builder)
	{
		base.Configure(builder);

		builder.Property(x => x.FirstName).IsRequired();
		builder.Property(x => x.LastName).IsRequired();
		builder.Property(x => x.Birthdate).IsRequired();
		builder.Property(x => x.PhoneNumber).IsRequired();
		builder.Property(x => x.Email).IsRequired();

		builder
			.HasDiscriminator<string>("Team")
			.HasValue<Doctor>("Doctor")
			.HasValue<Assistant>("Assistant")
			.HasValue<Secretary>("Secretary");

		builder.HasMany(x => x.Availabilities)
			.WithOne();

		builder.HasOne(x => x.Bio)
			.WithOne(x => x.Employee)
			.HasForeignKey<Bio>(x => x.Id);
	}
}
