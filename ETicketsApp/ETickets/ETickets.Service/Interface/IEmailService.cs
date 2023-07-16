using ETickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETickets.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO eMail);
    }
}
