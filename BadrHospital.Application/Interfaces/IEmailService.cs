using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendAsync(string toEmail, string subject, string htmlBody);
    }
}
