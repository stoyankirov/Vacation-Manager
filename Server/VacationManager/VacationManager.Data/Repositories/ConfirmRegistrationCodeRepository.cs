namespace VacationManager.Data.Repositories
{
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;

    public class ConfirmRegistrationCodeRepository : Repository<ConfirmRegistrationCode>, IConfirmRegistrationCodeRepository
    {
        public ConfirmRegistrationCodeRepository(VacationManagerContext dbContext) : base(dbContext)
        {
        }
    }
}
