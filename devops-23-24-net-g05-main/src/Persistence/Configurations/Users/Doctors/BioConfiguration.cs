using Domain.Users.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Users.Doctors;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.Users.Doctors;

internal class BioConfiguration : EntityConfiguration<Bio>
{
	public override void Configure(EntityTypeBuilder<Bio> builder)
	{
		builder.Property(x => x.Info);
	}
}
