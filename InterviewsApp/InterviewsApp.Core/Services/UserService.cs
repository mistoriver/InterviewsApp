using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.WebAPI.Models;
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

        public void CreateUser(CreateUserDto dto)
        {
            if (!IsUnique(dto))
            {
                var user = _mapper.Map<UserEntity>(dto);
                user.Password = _passwordService.HashPassword(user.Password);
                user.IsActive = true;
                _repository.Create(user);
            }
        }
        public bool IsUnique(CreateUserDto dto) => _repository.Get(user => user.Login.Equals(dto.Login)).Any();

        public LoginDto Login(LoginUserDto dto)
        {
            var user = _repository.Get(u => u.Login == dto.Login).FirstOrDefault();

            if (user != null)
            {
                if(_passwordService.VerifyPassword(dto.Password, user.Password))
                {
                    var token = _authService.Generate(dto.Login);
                    return new LoginDto() { Token = token, UserId = user.Id };
                }
            }
            return null;
        }
    }
}
