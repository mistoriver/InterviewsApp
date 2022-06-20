using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewsApp.Data.EntityConfigurations
{
    public class CompanyEntityConfiguration : BaseEntityConfiguration<CompanyEntity>
    {
        public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            base.Configure(builder);

            //Название таблицы
            builder.ToTable(nameof(InterviewsContext.Companies));

            builder.Property(entity => entity.Name).IsRequired();

            builder.HasMany(entity => entity.Positions).WithOne(entity => entity.Company);
        }
    }
}
