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
    public class NT_SubjectController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetSubjectRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));
            
            Profile? _profile = _user.GetProfile();
            if (_profile == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));

            SuperSubject? _superSubject = _profile.GetSuperSubject(_request.SuperSubjectID);
            if (_superSubject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve SuperSubject!")));

            List<Subject> _subjects = _superSubject.subjects;
            List<NetSubjectResponse> _response = new List<NetSubjectResponse>();

            foreach (Subject _subject in _subjects) {
                _response.Add(new NetSubjectResponse(
                    _subject.id_subject.ToString(), 
                    _subject.name,
                    _subject.gradeAverage.ToString("0.0")));
            }

            return Ok(JsonSerializer.Serialize(_response));
        }
    }

    public class NetSubjectRequest
    {
        [Required] public string Token { get; }
        [Required] public string SuperSubjectID { get; }

        public NetSubjectRequest(string token, string superSubjectID) {
            Token = token;
            SuperSubjectID = superSubjectID;
        }
    }

    public class NetSubjectResponse
    {
        [Required] public string Id { get; }
        [Required] public string Name { get; }
        [Required] public string Average { get; }

        public NetSubjectResponse(string id, string name, string average) {
            Id = id;
            Name = name;
            Average = average;
        }
    }
}
