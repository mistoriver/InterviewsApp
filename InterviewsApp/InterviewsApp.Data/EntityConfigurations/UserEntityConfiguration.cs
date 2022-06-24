using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewsApp.Data.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(InterviewsContext.Users));

            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.Login).IsRequired();
            builder.Property(entity => entity.Password).IsRequired();

            builder.HasMany(entity => entity.Positions).WithOne(entity => entity.User);
        }
    }
}
