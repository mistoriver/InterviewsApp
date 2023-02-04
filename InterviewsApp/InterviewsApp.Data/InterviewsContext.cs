using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace InterviewsApp.Data
{
    public class InterviewsContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CompanyEntity> Companies => Set<CompanyEntity>();
        public DbSet<PositionEntity> Positions => Set<PositionEntity>();
        public DbSet<InterviewEntity> Interviews => Set<InterviewEntity>();
        public DbSet<LocalizationEntity> Localizations => Set<LocalizationEntity>();


        public InterviewsContext(DbContextOptions<InterviewsContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
