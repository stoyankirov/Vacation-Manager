namespace VacationManager.Business.Factories
{
    using VacationManager.Domain.Responses;

    public static class AuthFactory
    {
        public static LoginResponse ToLoginResponse(this string jwt) =>
            jwt == null ? null :
            new LoginResponse
            {
                JWT = jwt
            };
    }
}
