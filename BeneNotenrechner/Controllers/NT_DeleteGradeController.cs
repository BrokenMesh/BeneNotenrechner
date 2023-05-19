using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeneNotenrechner.Controllers {
    [ApiController]
    [Route("nt/[controller]")]
    public class NT_DeleteGradeController : ControllerBase {
        [HttpPost]
        public IActionResult Post(NetDeleteGradeRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null)
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            Profile? _profile = _user.GetProfile();
            if (_profile == null)
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));

            SuperSubject? _superSubject = _profile.GetSuperSubject(_request.SuperSubjectID);
            if (_superSubject == null)
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve SuperSubject!")));

            Subject? _subject = _superSubject.GetSubject(_request.SubjectID);
            if (_subject == null)
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Subject!")));

            Grade? _grade = _subject.GetGrade(_request.GradeID);
            if (_grade == null)
                return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Grade!")));

            _grade.Delete();
            _subject.grades.Remove(_grade);

            _subject.EvaluateAverage();

            return Ok();
        }
    }

    public class NetDeleteGradeRequest {
        [Required] public string Token { get; }
        [Required] public string SubjectID { get; }
        [Required] public string SuperSubjectID { get; }
        [Required] public string GradeID { get; }

        public NetDeleteGradeRequest(string token, string subjectID, string superSubjectID, string gradeID) {
            Token = token;
            SubjectID = subjectID;
            SuperSubjectID = superSubjectID;
            GradeID = gradeID;
        }
    }
}
