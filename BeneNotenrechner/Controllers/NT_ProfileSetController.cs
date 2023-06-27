using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("nt/[controller]")]
    public class NT_ProfileSetController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetProfileSetRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            // Check if profile exists

            int _index;
            if(!int.TryParse(_request.Index, out _index))
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Index!")));

            _user.SetProfile(_index);

            return Ok();
        }
    }

    public class NetProfileSetRequest
    {
        [Required] public string Token { get; }
        [Required] public string Index { get; }

        public NetProfileSetRequest(string token, string index) {
            Token = token;
            Index = index;
        }
    }
}
