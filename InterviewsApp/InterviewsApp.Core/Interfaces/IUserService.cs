using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.WebAPI.Models;

namespace InterviewsApp.Core.Interfaces
{
    public interface IUserService : IDbService<UserEntity, UserDto>
    {
        /// <summary>
        /// Создать пользователя в системе
        /// </summary>
        /// <param name="dto">Данные для создания пользователя</param>
        public void CreateUser(CreateUserDto dto);
        /// <summary>
        /// Авторизовать пользователя в системе
        /// </summary>
        /// <param name="dto">Данные для авторизации</param>
        /// <returns></returns>
        public LoginDto Login(LoginUserDto dto);
    }
}
