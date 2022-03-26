namespace VacationManager.Data.Contracts
{
    using VacationManager.Domain.Entities;

    public interface IConfirmRegistrationCodeRepository
    {
        /// <summary>
        /// Add an entity.
        /// <param name="entity">The entity instance.</param>
        /// </summary>
        void AddAsync(ConfirmRegistrationCode entity);
    }
}
