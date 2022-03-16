namespace VacationManager.Data.Repositories
{
    using System.Linq;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(VacationManagerContext dbContext) : base(dbContext)
        {
        }

        public User GetUserByEmail(string email)
            => this.DbContext.Users
                .FirstOrDefault(u => u.Email == email);
    }
}
