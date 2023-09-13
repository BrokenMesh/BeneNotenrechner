using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System;
using System.Net.Http;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace BeneNotenrechner_MailAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TokenMailController : ControllerBase {

        static string demo = "";

        [HttpGet]
        public IActionResult Get() {
            return Ok(demo);
        }

        [HttpPost]
        public IActionResult Get(NetTokenEmail _request) {
            demo += _request.EMail;
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
