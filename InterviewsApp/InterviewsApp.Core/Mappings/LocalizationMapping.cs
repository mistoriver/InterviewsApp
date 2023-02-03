using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Mappings
{
    public class LocalizationMapping : BaseMapping<LocalizationEntity, LocalizationDto>
    {
        public LocalizationMapping() :base()
        {
            CreateMap<LocalizationEntity, LocalizationDto>();
            CreateMap<LocalizationDto, LocalizationEntity>();
        }
    }
}
