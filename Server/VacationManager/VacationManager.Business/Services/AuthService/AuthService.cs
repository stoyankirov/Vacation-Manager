namespace VacationManager.Business.Services.UserService
{
    using System;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Core.Utility;
    using VacationManager.Data.Contracts;
    using VacationManager.Domain.Entities;
    using VacationManager.Domain.Requests;

    public class AuthService : Service, IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public void Register(RegisterRequest request)
        {
            this.ValidateRequest(request);

            var passwordSalt = string.Empty;
            var passwordHash = PasswordHasher
                .Hash(request.Password, out passwordSalt);

            var userEntity = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt
            };

            this._userRepository.AddAsync(userEntity);
        }
    }
}
