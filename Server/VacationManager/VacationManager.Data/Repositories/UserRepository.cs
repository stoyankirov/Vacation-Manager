namespace VacationManager.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
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

        public async override Task<User> GetByIdAsync(Guid id)
        {
            var user = await base.GetByIdAsync(id);
            var codes = await this._confirmationCodeRepository.GetAllAsync();

            var confirmationCode = this.DbContext.ConfirmRegistrationCodes
                .FirstOrDefault(x => x.Id == user.ConfirmRegistrationCodeId);

            user.ConfirmRegistrationCode = confirmationCode;

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
            => await this.Entities
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email.ToUpper() == email.ToUpper());
    }
}
