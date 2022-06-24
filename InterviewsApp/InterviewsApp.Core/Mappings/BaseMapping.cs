using AutoMapper;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Core.Mappings
{
    public abstract class BaseMapping<TEntity, TExternalDto>: Profile
        where TEntity : BaseEntity
        where TExternalDto : BaseExternalDto
    {
        public BaseMapping()
        {
            CreateMap<TEntity, TExternalDto>();
        }
    }
}
