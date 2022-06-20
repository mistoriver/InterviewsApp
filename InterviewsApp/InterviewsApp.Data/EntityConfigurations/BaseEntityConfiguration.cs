using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewsApp.Data.EntityConfigurations
{
    /// <summary>
    /// Базовая конфигурация сущности
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        }
    }
}
