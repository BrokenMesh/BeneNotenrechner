using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp;
using RestSharp.Authenticators;

namespace FeatureTest {
    public class E_Mail_Demo 
    {
        private static Random random = new Random();
        private static SmtpClient smtpClient;

        public static void Run() {
            smtpClient = new SmtpClient("smtp.gmail.com") {
                Port = 587,
                Credentials = new NetworkCredential("elkordhicham55demo@gmail.com", "evhqqxffufyogqqp"),
                EnableSsl = true,
            };

            string _token = GenerateRandomToken();
            SendTokenMail(_token);

            Console.WriteLine("Enter Token: ");
            string _usertoken = Console.ReadLine();

            if (_token == _usertoken.Trim()) {
                Console.WriteLine("It is correct you have entered ");
            }

        }


        private static void SendTokenMail(string _token) {

            var _mailMessage = new MailMessage {
                From = new MailAddress("elkordhicham55demo@gmail.com"),
                Subject = "Demo Code",
                Body = $"<h1>Code:</h1><br><h2>{_token}</h2>",
                IsBodyHtml = true,
            };

            _mailMessage.To.Add("elkordhicham@gmail.com");

            smtpClient.Send(_mailMessage);

        }

        private static string GenerateRandomToken() {
            return random.Next(100_000, 999_999).ToString();
        }

    }
}
