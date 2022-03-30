namespace VacationManager.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IConfirmRegistrationCodeRepository _confirmationCodeRepository;
        public UserRepository(VacationManagerContext dbContext, IConfirmRegistrationCodeRepository confirmationCodeRepository)
            : base(dbContext)
        {
            this._confirmationCodeRepository = confirmationCodeRepository;
        }

        public override Task<User> GetByIdAsync(Guid id)
        {
            var user = base.GetByIdAsync(id);
            var codes = this._confirmationCodeRepository.GetAllAsync();

            var confirmationCode = this.DbContext.ConfirmRegistrationCodes
                .FirstOrDefault(x => x.Id == user.Result.ConfirmRegistrationCodeId);

            user.Result.ConfirmRegistrationCode = confirmationCode;

            return user;
        }

        public User GetUserByEmail(string email)
            => this.DbContext.Users
                .FirstOrDefault(u => u.Email == email);
    }
}
