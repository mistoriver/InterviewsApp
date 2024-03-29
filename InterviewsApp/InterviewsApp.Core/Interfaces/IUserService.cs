﻿using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Interfaces
{
    public interface IUserService : IDbService<UserEntity, UserDto>
    {
        /// <summary>
        /// Создать пользователя в системе
        /// </summary>
        /// <param name="dto">Данные для создания пользователя</param>
        public Task<Response<Guid>> CreateUser(CreateUserDto dto);
        /// <summary>
        /// Авторизовать пользователя в системе
        /// </summary>
        /// <param name="dto">Данные для авторизации</param>
        /// <returns></returns>
        public Task<Response<LoginDto>> Login(LoginUserDto dto);
    }
}
