namespace VacationManager.Data.Repositories
{
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(VacationManagerContext dbContext) : base(dbContext)
        {
        }
    }
}
