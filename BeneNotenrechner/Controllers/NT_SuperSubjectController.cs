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
            int _userId = UserManager.GetUserIdFromToken(uint.Parse(_request.Token));

            if (_userId == -1) {
                return Ok(JsonSerializer.Serialize(new NetSuperSubjectError("Could not resolve User!")));
            }

            Profile? _profile = DBManager.instance.GetProfile(_userId, false);

            if (_profile == null) {
                return Ok(JsonSerializer.Serialize(new NetSuperSubjectError("Could not resolve Profile!")));
            }

            List<SuperSubject> _superSubjects = DBManager.instance.GetSuperSubjectAll(_profile, false);
            List<NetSuperSubjectResponse> _response = new List<NetSuperSubjectResponse>();

            foreach (SuperSubject _superSubject in _superSubjects) {
                _response.Add(new NetSuperSubjectResponse(
                    _superSubject.id_supersubject.ToString(), _superSubject.name, _superSubject.semester.ToString()));
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

    public class NetSuperSubjectError {
        [Required] public string Error { get; }
        public NetSuperSubjectError(string error) {
            Error = error;
        }
    }

}
