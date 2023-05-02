using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReauthenticateController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetReauthenticate _value) {
            UserManager.ReauthenticateUser(uint.Parse(_value.Token));
            return Ok();
        }
    }

    public class NetReauthenticate {
        [Required] public string Token { get; }
        public NetReauthenticate(string token) {
            Token = token;
        }
    }

}
