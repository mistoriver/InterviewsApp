using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs.External
{
    public class InterviewUiDto : InterviewDto
    {
        public string PositionName { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
