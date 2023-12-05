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

        public void NotifyStudentRegistration(string studentName, string studentEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Trung Tâm Tin Học 2H", "duchuyy16@gmail.com"));
            message.To.Add(new MailboxAddress(studentName, studentEmail));
            message.Subject = "Xác Nhận Đăng Ký Khóa Học";
            message.Body = new TextPart("html")
            {
                Text = "<h1>Xác Nhận Đăng Ký Khóa Học</h1>" +
                       "<p>Xin chào " + studentName + "!</p>" +
                       "<p>Cảm ơn bạn đã đăng ký khóa học tại Trung Tâm Tin Học 2H. Chúng tôi đã nhận được thông tin đăng ký của bạn.</p>" +
                       "<p>Khi nhận được email này, bạn hãy phản hồi lại cho mình các thông tin cụ thể của học viên gồm:</p>" +
                       "<ul>" +
                       "   <li>Họ và tên</li>" +
                       "   <li>Ngày tháng năm sinh</li>" +
                       "   <li>Nơi sinh (Tỉnh)</li>" +
                       "   <li>Số điện thoại</li>" +
                       "   <li>Email </li>" +
                       "   <li>Giới tính</li>" +
                       "</ul>" +
                       "<p>Các thông tin học viên sẽ được cập nhật vào danh sách lớp, tại hệ thống thông tin học viên của Trung Tâm và thông tin để cấp chứng chỉ khi kết thúc khóa học nên bạn phản hồi sớm và chính xác giúp mình nhé.</p>" +
                       "<p><strong>Khi cần hỗ trợ, bạn vui lòng liên lạc Phòng tư vấn-ghi danh:</strong></p>" +
                       "<ul>" +
                       "   <li>(+84) 090 888 777</li>" +
                       "   <li>Thứ 2 - thứ 6: 07h30 - 11h30 và 13h00 - 19h00</li>" +
                       "   <li>Riêng thứ 7-chủ nhật: 07h30 - 11h30 và 13h00 - 17h00</li>" +
                       "</ul>" +
                       "<p><strong>Nếu có yêu cầu xuất hóa đơn tài chính, bạn liên hệ Phòng Ghi Danh (tại TTTH-128 Nguyễn Sơn Q.Tân Phú) để làm thủ tục trong tuần lễ khai giảng.</strong></p>" +
                       "<p><strong>Nếu có bất kỳ thay đổi gì về lịch học, Trung Tâm sẽ liên hệ trực tiếp với bạn.</strong></p>" +
                       "<p>Mến chúc bạn nhiều sức khoẻ và thành công.</p>" +
                       "<p>Trân trọng!</p>"
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
