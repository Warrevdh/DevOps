using Domain.EyeConditions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.EyeConditions;

internal class EyeConditionConfiguration : EntityConfiguration<EyeCondition>
{
	public override void Configure(EntityTypeBuilder<EyeCondition> builder)
	{
		base.Configure(builder);

        //builder.HasKey(ec => ec.Id); // Define the primary key

        builder
            .HasMany(x => x.Symptoms)
            .WithMany(x => x.Conditions)
            .UsingEntity(j => j.ToTable("EyeConditionSymptoms"));

        builder.Property(ec => ec.Name);
		//    .IsRequired()
		//    .HasMaxLength(255); // Configure the 'Name' property

		builder.Property(ec => ec.Description);
		//    .HasMaxLength(1000); // Configure the 'Description' property

		builder.Property(ec => ec.ImageUrl);
	}
}