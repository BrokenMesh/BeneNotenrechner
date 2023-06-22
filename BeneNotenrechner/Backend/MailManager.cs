using System.Net;
using System.Net.Mail;

namespace BeneNotenrechner.Backend {
    public class MailManager 
    {
        private static SmtpClient smtpClient;
        private static string hostMail;

        private static string tokenMailHTML;

        public static MailManager Instance { get; private set; }
        
        public MailManager(string _SMTPServer, string _hostMail, string _hostPassword) {
            if (Instance != null) {
                throw new InvalidOperationException("There cannot be more the one instance of the MailManager");
            }

            hostMail = _hostMail;

            smtpClient = new SmtpClient(_SMTPServer) {
                Port = 587,
                Credentials = new NetworkCredential(_hostMail, _hostPassword),
                EnableSsl = true,
            };

            LoadHTML("");
        }

        public MailManager(Config _config) {
            if (Instance != null) {
                throw new InvalidOperationException("There cannot be more the one instance of the MailManager");
            }

            hostMail = _config.MAIL_HostMail;

            smtpClient = new SmtpClient(_config.MAIL_SMTPServer) {
                Port = 587,
                Credentials = new NetworkCredential(_config.MAIL_HostMail, _config.MAIL_HostPassword),
                EnableSsl = true,
            };

            LoadHTML(_config.MAIL_HTMLPath);
        }

        private void LoadHTML(string _path) {
            try {
                tokenMailHTML = File.ReadAllText(_path);
            } catch (Exception) {
                tokenMailHTML = "<h1>Code:</h1><h2>%token%</h2>";
            }
        }

        public void SendTokenMail(string _email, string _token) {

            var _mailMessage = new MailMessage {
                From = new MailAddress(hostMail),
                Subject = "Authenticate E-Mail",
                Body = tokenMailHTML.Replace("%token%", _token),
                IsBodyHtml = true,
            };

            _mailMessage.To.Add(_email);

            smtpClient.Send(_mailMessage);
        }

    }
}
