using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Vieon.Models;

namespace Vieon.Controllers.Observer
{
    public class EmailObserver : IObserver
    {
        private readonly string smtpServer;
        private readonly int port;
        private readonly string senderEmail;
        private readonly string password;

        public EmailObserver(string smtpServer, int port, string senderEmail, string password)
        {
            this.smtpServer = smtpServer;
            this.port = port;
            this.senderEmail = senderEmail;
            this.password = password;
        }

        public void Update(PhimYeuThich phimYeuThich, TapPhim tapPhimMoi)
        {
            string recipientEmail = phimYeuThich.User.Email;
            string subject = "Thông báo tập phim mới";
             string body = $"Xin chào,\n\nBộ phim {tapPhimMoi.Phim.TenPhim} mà bạn yêu thích đã có tập phim mới. Hãy truy cập vào trang web của chúng tôi để xem.\n\nTrân trọng,";

            SendEmail(recipientEmail, subject, body);
        }

        private void SendEmail(string recipientEmail, string subject, string body)
        {
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, port))
            {
                smtpClient.Credentials = new NetworkCredential(senderEmail, password);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body))
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}