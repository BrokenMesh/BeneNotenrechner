using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System;
using System.Net.Http;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Dev_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase {

        public const string HOST = "smtp.gmail.com";
        public const string HOSTMAIL = "elkordhicham55demo@gmail.com";
        public const string PASSWORD = "evhqqxffufyogqqp";


        [HttpPost]
        public IActionResult Get(NetTokenEmail _request) {
            try {
                SmtpClient smtpClient = new SmtpClient(HOST) {
                    Port = 587,
                    Credentials = new NetworkCredential(HOSTMAIL, PASSWORD),
                    EnableSsl = true,
                };

                var _mailMessage = new MailMessage {
                    From = new MailAddress("elkordhicham55demo@gmail.com"),
                    Subject = "Demo Code",
                    Body = $"<h1>Code:</h1><br><h2>{_request.Token}</h2>",
                    IsBodyHtml = true,
                };

                _mailMessage.To.Add(_request.EMail);

                smtpClient.Send(_mailMessage);
            } catch (Exception) {
                return BadRequest();
            }

            return Ok();
        }

        public class NetTokenEmail {
            [Required] public string EMail { get; }
            [Required] public string Token { get; }

            public NetTokenEmail(string eMail, string token) {
                EMail = eMail;
                Token = token;
            }
        }

    }
}
