using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Twajd_Back_End.Core.Services
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
