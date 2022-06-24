using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewsApp.Data.EntityConfigurations
{
    public class PositionEntityConfiguration : BaseEntityConfiguration<PositionEntity>
    {
        public override void Configure(EntityTypeBuilder<PositionEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(InterviewsContext.Positions));

            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.City).IsRequired();

            builder.HasOne(entity => entity.Company).WithMany(entity => entity.Positions);
            builder.HasMany(entity => entity.Interviews).WithOne(entity => entity.Position);
            builder.HasOne(entity => entity.User).WithMany(entity => entity.Positions);

        }
    }
}
