using BadrHospital.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BadrHospital.Infrastructure.Services
{
    public class EmailService(IConfiguration config) : IEmailService
    {
        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var apiKey = config["Email:SendGridApiKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(config["Email:From"], config["Email:FromName"]);
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent: StripHtml(htmlBody), // fallback for clients that don't render HTML
                htmlContent: htmlBody);

            var response = await client.SendEmailAsync(msg);

            if ((int)response.StatusCode >= 400)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"Failed to send email. Status: {response.StatusCode}, Body: {body}");
            }
        }

        private static string StripHtml(string html)
            => System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
    }
}
