using InterviewsApp.Core.DTOs;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    public interface IInterviewService
    {
        public IEnumerable<InterviewEntity> Get(Guid id);
        public void CreateInterview(CreateInterviewDto dto);
    }
}
