using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

namespace Services.MailKit
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;
        public MailService(MailSettings settings)
        {
            _settings = settings;
        }

        public void SendMail(string name, string from, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bui Duc Huy", "duchuyy16@gmail.com"));
            message.To.Add(new MailboxAddress(name, from));
            message.Subject = "Mail From Contact Us";
            message.Body = new TextPart("html")
            {
                Text = "<h1>Thông tin liên hệ mới</h1><br/>" +
                       "<p><strong>Tên người liên hệ:</strong> " + name + "</p>" +
                       "<p><strong>Email người liên hệ:</strong> " + from + "</p>" +
                       "<p><strong>Nội dung:</strong> " + body + "</p>"
            };
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_settings.Email, _settings.Password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
