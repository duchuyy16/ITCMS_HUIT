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
using System.Security.Cryptography.X509Certificates;

namespace Services.MailKit
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;
        public MailService(MailSettings settings)
        {
            _settings = settings;
        }

        public static bool DefaultServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // If there are no SSL policy errors, then the certificate is considered valid
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Ignore revocation errors
            chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

            // If the certificate is valid despite revocation errors, consider it valid
            return sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors && chain.Build((X509Certificate2)certificate);
        }


        public void SendMail(string name, string from, int? body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Trung Tâm Tin Học 2H", "duchuyy16@gmail.com"));
            message.To.Add(new MailboxAddress(name, from));
            message.Subject = "Thông Báo Vắng Học";
            message.Body = new TextPart("html")
            {
                Text = "<h1>Thông Báo Vắng Học</h1><br/>" +
                       "<p><strong>Học viên:</strong> " + name + "</p>" +
                       "<p><strong>Email:</strong> " + from + "</p>" +
                       "<p><strong>Số lần vắng học:</strong> " + body + "</p>" +
                       "<p>Xin thông báo rằng học sinh đã vắng học " + body + " lần. Hãy liên hệ với trung tâm để biết thêm chi tiết.</p>"
            };
            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = DefaultServerCertificateValidationCallback!;
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_settings.Username, _settings.Password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
