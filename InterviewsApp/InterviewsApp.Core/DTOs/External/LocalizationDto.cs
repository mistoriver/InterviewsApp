using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs.External
{
    public class LocalizationDto : BaseExternalDto
    {
        public string LocalizationCode { get; set; }

        public string Value { get; set; }
    }
}
