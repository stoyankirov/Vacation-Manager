namespace VacationManager.Domain.Requests
{
    using System;

    public class ConfirmRegistrationRequest : Request
    {
        public Guid UserId { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
