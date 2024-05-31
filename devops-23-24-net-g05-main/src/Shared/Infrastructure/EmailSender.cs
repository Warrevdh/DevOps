using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = "oogarts.ivision@gmail.com";
        var pw = "8&zMdE3vN4VK&EJf3%t!Tbx@XL#YHv";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(email, pw)
        };

        return client.SendMailAsync(
            new MailMessage(
                from: mail,
                to: email,
                subject,
                message
            )
       );


    }
}
