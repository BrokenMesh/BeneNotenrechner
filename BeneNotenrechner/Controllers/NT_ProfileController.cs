using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("nt/[controller]")]
    public class NT_ProfileController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetProfileRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            List<Profile> _profiles = DBManager.instance.GetProfileAll(_user);
            List<NetProfileResponse> _response = new List<NetProfileResponse>();

            foreach (Profile _profile in _profiles) {
                _response.Add(new NetProfileResponse(
                        _profile.id_profile.ToString(),
                        _profile.index.ToString()
                    ));
            }

            return Ok(JsonSerializer.Serialize(_response));
        }
    }

    public class NetProfileRequest
    {
        [Required] public string Token { get; }

        public NetProfileRequest(string token) {
            Token = token;
        }
    }

    public class NetProfileResponse
    {
        [Required] public string Id { get; }
        [Required] public string Index { get; }

        public NetProfileResponse(string id, string index) {
            Id = id;
            Index = index;
        }
    }
}
