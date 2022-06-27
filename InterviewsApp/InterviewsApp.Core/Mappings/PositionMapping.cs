using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Core.Mappings
{
    public class PositionMapping : BaseMapping<PositionEntity, PositionDto>
    {
        public PositionMapping() : base()
        {
            CreateMap<CreatePositionDto, PositionEntity>();
            CreateMap<PositionDto, PositionUiDto>();
        }
    }
}
