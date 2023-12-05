using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MailKit
{
    public interface IMailService
    {
        void SendMail(string name, string from, int? body);
        void NotifyStudentRegistration(string studentName, string studentEmail);
    }
}
