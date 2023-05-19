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
    public class NT_GradeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetGradeRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            Profile? _profile = _user.GetProfile();
            if (_profile == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));

            SuperSubject? _superSubject = _profile.GetSuperSubject(_request.SuperSubjectID);
            if (_superSubject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve SuperSubject!")));

            Subject? _subject = _superSubject.GetSubject(_request.SubjectID);
            if (_subject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Subject!")));

            List<Grade> _grades = _subject.grades;
            List<NetGradeResponse> _response = new List<NetGradeResponse>();

            foreach (Grade _grade in _grades) {
                _response.Add(new NetGradeResponse(
                    _grade.id_grade.ToString(), 
                    _grade.grade, 
                    _grade.evaluation,
                    _grade.date.ToShortDateString(), 
                    _grade.name));
            }

            return Ok(JsonSerializer.Serialize(_response));
        }
    }

    public class NetGradeRequest
    {
        [Required] public string Token { get; }
        [Required] public string SuperSubjectID { get; }
        [Required] public string SubjectID { get; }

        public NetGradeRequest(string token, string superSubjectID, string subjectID) {
            Token = token;
            SuperSubjectID = superSubjectID;
            SubjectID = subjectID;
        }
    }

    public class NetGradeResponse
    {
        [Required] public string Id { get; }
        [Required] public float Grade { get; }
        [Required] public float Evaluation { get; }
        [Required] public string Date { get; }
        [Required] public string Name { get; }

        public NetGradeResponse(string id, float grade, float evaluation, string date, string name) {
            Id = id;
            Grade = grade;
            Evaluation = evaluation;
            Date = date;
            Name = name;
        }
    }
}
