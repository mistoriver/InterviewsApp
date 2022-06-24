using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;

namespace InterviewsApp.Core.Mappings
{
    public class UserMapping : BaseMapping<UserEntity, UserDto>
    {
        public UserMapping() : base()
        { 
            CreateMap<UserEntity, CreateUserDto>();
        }
    }
}
