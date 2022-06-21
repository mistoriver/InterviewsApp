using InterviewsApp.Core.DTOs;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public class UserService :IUserService
    {
        private readonly IRepository<UserEntity> _repository;

        public UserService(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserEntity> Get(Guid id)
        {
            if (id != Guid.Empty)
                return _repository.GetByPredicate(entity => entity.Id.Equals(id));
            return _repository.GetByPredicate(e => true);
        }

        public void CreateUser(CreateUserDto dto)
        {
            _repository.Create(new UserEntity() { Id = new Guid(), Login = dto.Login, Password = dto.Password, Name = dto.Name, IsActive = true }) ;
        }
    }
}
