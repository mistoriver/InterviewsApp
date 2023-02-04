using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Seeding
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly InterviewsContext _interviewsContext;
        public DatabaseSeeder(InterviewsContext context) 
        {
            _interviewsContext = context;
        }

        public void SeedDatabase()
        {
            var locals = GetDefaultLocalsFromJson();
            _interviewsContext.RemoveRange(locals);
            _interviewsContext.SaveChanges();
            _interviewsContext.AddRange(locals);
            _interviewsContext.SaveChanges();
        }
        private LocalizationEntity[] GetDefaultLocalsFromJson()
        {
            var json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DefaultLocalizations.json");
            var locals = JsonSerializer.Deserialize<LocalizationEntity[]>(json);
            return locals;
        }
    }
}
