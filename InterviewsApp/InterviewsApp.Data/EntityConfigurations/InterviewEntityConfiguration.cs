using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewsApp.Data.EntityConfigurations
{
    public class InterviewEntityConfiguration : BaseEntityConfiguration<InterviewEntity>
    {
        public override void Configure(EntityTypeBuilder<InterviewEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(InterviewsContext.Interviews));

            builder.Property(entity => entity.Name).IsRequired();

            builder.HasOne(entity => entity.Position).WithMany(entity => entity.Interviews);
        }
    }
}
