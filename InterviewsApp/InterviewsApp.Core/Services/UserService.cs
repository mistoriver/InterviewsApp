using AutoMapper;
using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class UserService : BaseDbService<UserEntity, UserDto>, IUserService
    {
        private readonly IPasswordService _passwordService;

        public UserService(IRepository<UserEntity> repository, IPasswordService passwordService, IMapper mapper) : base(repository, mapper)
        {
            _passwordService = passwordService;
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
    }
}
