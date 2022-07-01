using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.WebAPI.Models;
using System;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class UserService : BaseDbService<UserEntity, UserDto>, IUserService
    {
        private readonly IPasswordService _passwordService;
        private readonly IAuthService _authService;

        public UserService(IRepository<UserEntity> repository, IPasswordService passwordService, IAuthService authService, IMapper mapper) : base(repository, mapper)
        {
            _passwordService = passwordService;
            _authService = authService;
        }

        public Response<Guid> CreateUser(CreateUserDto dto)
        {
            if (!IsUnique(dto))
            {
                var user = _mapper.Map<UserEntity>(dto);
                user.Password = _passwordService.HashPassword(user.Password);
                user.IsActive = true;
                return new Response<Guid>(_repository.Create(user));
            }
            return new Response<Guid>("Пользователь с данным логином уже существует");
        }
        public bool IsUnique(CreateUserDto dto) => _repository.Get(user => user.Login.Equals(dto.Login)).Any();

        public Response<LoginDto> Login(LoginUserDto dto)
        {
            var user = _repository.Get(u => u.Login == dto.Login).FirstOrDefault();

            if (user != null)
            {
                if(_passwordService.VerifyPassword(dto.Password, user.Password))
                {
                    var token = _authService.Generate(dto.Login);
                    return new Response<LoginDto>(new LoginDto() { Token = token, UserId = user.Id });
                }
            }
            return new Response<LoginDto>("Неправильный логин и/или пароль");
        }
    }
}
