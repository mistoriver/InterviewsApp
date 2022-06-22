using InterviewsApp.Core.DTOs;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    public interface IUserService : IDbService<UserEntity>
    {
        /// <summary>
        /// Создать пользователя в системе
        /// </summary>
        /// <param name="dto"></param>
        public void CreateUser(CreateUserDto dto);
    }
}
