namespace VacationManager.Domain.Entities
{
    public partial class User
    {
        public User(bool isConfirmed)
        {
            this.IsConfirmed = isConfirmed;
        }

        public User Copy(User user)
        {
            if (user != null)
            {
                this.Id = user.Id;
                this.ConfirmRegistrationCodeId = user.ConfirmRegistrationCodeId;
                this.Email = user.Email;
                this.Password = user.Password;
                this.PasswordSalt = user.PasswordSalt;
                this.Role = user.Role;
                this.IsBanned = user.IsBanned;
                this.IsConfirmed = true;
                this.RegistrationDate = user.RegistrationDate;
            };

            return this;
        }
    }
}
