using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("nt/[controller]")]
    public class NT_SuperSubjectController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetSuperSubjectRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));
            
            Profile? _profile = _user.GetProfile();
            if (_profile == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));
            
            List<SuperSubject> _superSubjects = _profile.superSubjects;
            List<NetSuperSubjectResponse> _response = new List<NetSuperSubjectResponse>();

            foreach (SuperSubject _superSubject in _superSubjects) {
                _response.Add(new NetSuperSubjectResponse(
                    _superSubject.id_supersubject.ToString(), 
                    _superSubject.name, 
                    _superSubject.semester.ToString()));
            }

            return Ok(JsonSerializer.Serialize(_response));
        }
    }

    public class NetSuperSubjectRequest {
        [Required] public string Token { get; }
        [Required] public string Profile { get; }

        public NetSuperSubjectRequest(string token, string profile) {
            Token = token;
            Profile = profile;
        }
    }

    public class NetSuperSubjectResponse {
        [Required] public string Id { get; }
        [Required] public string Name { get; }
        [Required] public string Semester { get; }

        public NetSuperSubjectResponse(string id, string name, string semester) {
            Id = id;
            Name = name;
            Semester = semester;
        }
    }

    public class NetError {
        [Required] public string Error { get; }
        public NetError(string error) {
            Error = error;
        }
    }
}
