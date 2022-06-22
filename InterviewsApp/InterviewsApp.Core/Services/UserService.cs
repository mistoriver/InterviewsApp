using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewsApp.Core.Services
{
    public class UserService : BaseDbService<UserEntity>, IUserService
    {
        private readonly IPasswordService _passwordService;

        public UserService(IRepository<UserEntity> repository, IPasswordService passwordService) :base(repository)
        {
            _passwordService = passwordService;
        }

        public void CreateUser(CreateUserDto dto)
        {
            if (!IsUnique(dto))
                _repository.Create(new UserEntity() { Id = new Guid(), Login = dto.Login, Password = _passwordService.HashPassword(dto.Password), Name = dto.Name, IsActive = true }) ;
        }
        public bool IsUnique(CreateUserDto dto) => _repository.Get(user => user.Login.Equals(dto.Login)).Any();
    }
}
