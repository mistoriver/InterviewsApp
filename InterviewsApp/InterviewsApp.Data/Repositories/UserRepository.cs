using InterviewsApp.Data.Abstractions;
using InterviewsApp.Data.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InterviewsApp.Data.Repositories
{
    /// <summary>
    /// Репозиторий для доступа к сущности <see cref="UserEntity"/>
    /// </summary>
    public class UserRepository : BaseRepository<UserEntity>
    {
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return AppContext.Users.ToList();
        }

        private bool IsUniqueLogin(string login)
        {
            return AppContext.Users.FirstOrDefault(u => u.Login == login && u.IsActive) == null;
        }

        public UserRepository(InterviewsContext context) : base(context)
        { }
    }
}
