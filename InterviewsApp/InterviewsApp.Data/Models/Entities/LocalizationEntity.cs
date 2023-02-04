using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Data.Models.Entities
{
    public class LocalizationEntity : BaseEntity
    {
        public new string Id { get; set; }
        public string Language { get; set; }

        public string Value { get; set; }
    }
}
