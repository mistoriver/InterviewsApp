using InterviewsApp.Data.Abstractions;
using InterviewsApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<UserEntity>> GetAllUsers()
        {
            return await AppContext.Users.ToListAsync();
        }

        private async Task<bool> IsUniqueLogin(string login)
        {
            return await AppContext.Users.FirstOrDefaultAsync(u => u.Login == login && u.IsActive) == null;
        }

        public UserRepository(InterviewsContext context) : base(context)
        { }
    }
}
