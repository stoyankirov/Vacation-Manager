namespace VacationManager.Core.Utility
{
    using System;
    using VacationManager.Domain.Entities;

    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string token);
    }
}
