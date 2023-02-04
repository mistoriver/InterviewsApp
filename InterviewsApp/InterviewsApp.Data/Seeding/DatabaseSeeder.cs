using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
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

            foreach (var loc in locals)
            {
                var locDb = _interviewsContext.Find(typeof(LocalizationEntity), loc.Id, loc.Language);
                if (locDb != null)
                {
                    _interviewsContext.Remove(locDb);
                    _interviewsContext.SaveChanges();
                }
                _interviewsContext.Add(loc);
                _interviewsContext.SaveChanges();
            }
            //try
            //{
            //    _interviewsContext.RemoveRange(locals);
            //    _interviewsContext.SaveChanges();
            //}
            //catch(DbUpdateConcurrencyException ex) { }
            //_interviewsContext.AddRange(locals);
            //_interviewsContext.SaveChanges();
        }
        private LocalizationEntity[] GetDefaultLocalsFromJson()
        {
            var json = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DefaultLocalizations.json");
            var locals = JsonSerializer.Deserialize<LocalizationEntity[]>(json);
            return locals;
        }
    }
}
