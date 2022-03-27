namespace VacationManager.Business.Services.Notification
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;
    using System.Net.Mail;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Domain.Models.Configuration;

    public class NotificationService : INotificationService
    {
        private readonly BusinessEmailCredentials _credentials;

        public NotificationService(IOptions<BusinessEmailCredentials> credentials)
        {
            this._credentials = credentials.Value ?? throw new ArgumentException(nameof(BusinessEmailCredentials));
        }

        public void SendRegisterConfirmationEmail(string email, string code)
        {
            var subject = "Sign-up Verification";

            var businessEmailLogin = this._credentials.Email;
            var businessEmailPassword = this._credentials.Password;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(businessEmailLogin);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = code;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(businessEmailLogin, businessEmailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
