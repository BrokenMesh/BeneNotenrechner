using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;

namespace BeneNotenrechner_MailAPI
{
    public class MailClient
    {
        private string host;
        private string userMail;
        private string password;

        private string tokenMailHtml;

        public static MailClient instance { get; private set; }
        public MailClient(Config _config) {
            instance = this;

            host = _config.Mail_Host;
            userMail = _config.Mail_UserMail;
            password = _config.Mail_Password;

            LoadHtml(_config);
        }

        private void LoadHtml(Config _config) {
            tokenMailHtml = "<h1>Code:</h1><br><h2>%token%</h2>";

            try {
                string _file = File.ReadAllText(_config.Mail_TokenMailHtmlPath);
                if (_file.Contains("%token%")) {
                    tokenMailHtml = _file;
                }
            }
            catch (Exception _e) {
                Console.WriteLine("MailClient: Failed to load TokenMail Html file: " + _e);
            }
        }

        public bool SendTokenMail(string _mail, string _token) {
            try {
                SmtpClient smtpClient = new SmtpClient(host)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(userMail, password),
                    EnableSsl = true,
                };

                var _mailMessage = new MailMessage
                {
                    From = new MailAddress("elkordhicham55demo@gmail.com"),
                    Subject = "Benedict SchoolTool: Authentication Code",
                    Body = tokenMailHtml.Replace("%token%", _token),
                    IsBodyHtml = true,
                };

                _mailMessage.To.Add(_mail);

                smtpClient.Send(_mailMessage);

                return true;
            }
            catch (Exception _e) {
                Console.WriteLine("MailClient: Sending e-mail failed: " + _e);
                return false;
            }
        }



    }
}
