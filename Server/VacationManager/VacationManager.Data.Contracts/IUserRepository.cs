﻿namespace VacationManager.Data.Contracts
{
    using VacationManager.Domain.Entities;

    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
    }
}