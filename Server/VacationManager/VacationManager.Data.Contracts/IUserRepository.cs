namespace VacationManager.Data.Contracts
{
    using System.Threading.Tasks;
    using VacationManager.Domain.Entities;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
