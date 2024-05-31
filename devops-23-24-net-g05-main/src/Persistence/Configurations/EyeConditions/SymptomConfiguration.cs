using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.EyeConditions;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.EyeConditions;

internal class SymptomConfiguration : EntityConfiguration<Symptom>
{
    public override void Configure(EntityTypeBuilder<Symptom> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(50);
    }
}
