using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginCallController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetUser _value) {
            Tuple<string, string> _result = UserManager.LoginUser(_value.Username, _value.Password);

            NetToken _token = new NetToken(_result.Item1, _result.Item2);
            string _response = JsonSerializer.Serialize(_token);
            return Ok(_response);
        }
    }

    public class NetUser {
        [Required] public string Username { get; }
        [Required] public string Password { get; }

        public NetUser(string username, string password) {
            Username = username;
            Password = password;
        }
    }

    public class NetToken {
        public string error { get; }
        public string token { get; }

        public NetToken(string token, string error) {
            this.token = token;
            this.error = error;
        }
    }
}
