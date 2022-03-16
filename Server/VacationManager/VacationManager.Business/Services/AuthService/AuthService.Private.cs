namespace VacationManager.Business.Services.AuthService
{
    using System;
    using VacationManager.Business.Contracts.Services;

    public partial class AuthService : Service, IAuthService
    {
        private void ValidateUserExistance(string email)
        {
            var user = this._userRepository
                .GetUserByEmail(email);

            if (user != null)
            {
                throw new ArgumentException("Email is already taken.");
            }
        }
    }
}
