using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterCallController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetRegister NetRegister) {

            string _error = UserManager.CreateTempUser(NetRegister.Username, NetRegister.Password, NetRegister.Usermail);

            return Ok(new NetError(_error));  
        }

    }

    public class NetRegister
    {
        [Required] public string Username { get; }
        [Required] public string Password { get; }
        [Required] public string Usermail { get; }

        public NetRegister(string username, string password, string usermail) {
            Username = username;
            Password = password;
            Usermail = usermail;
        }
    }   

}
