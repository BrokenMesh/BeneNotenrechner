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
    public class TokenMailController : ControllerBase {

        [HttpPost]
        public IActionResult Get(NetTokenEmail _request) {

            bool _result = MailClient.instance.SendTokenMail(_request.EMail, _request.Token);

            return _result ? Ok() : BadRequest();
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
