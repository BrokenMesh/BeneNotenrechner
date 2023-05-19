using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeneNotenrechner.Controllers
{
    [ApiController]
    [Route("nt/[controller]")]
    public class NT_CreateGradeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetCreateGradRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            Profile? _profile = _user.GetProfile();
            if (_profile == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));

            SuperSubject? _superSubject = _profile.GetSuperSubject(_request.SuperSubjectID);
            if (_superSubject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve SuperSubject!")));

            Subject? _subject = _superSubject.GetSubject(_request.SubjectID);
            if (_subject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Subject!")));

            _subject.CreateGrade(_request.Grade_Name, _request.Grade_Grade, _request.Grade_Evaluation, _request.Grade_Date);
            _subject.EvaluateAverage();

            return Ok();
        }
    }

    public class NetCreateGradRequest
    {
        [Required] public string Token { get; }
        [Required] public string SubjectID { get; }
        [Required] public string SuperSubjectID { get; }
        [Required] public string Grade_Name { get; }
        [Required] public string Grade_Grade { get; }
        [Required] public string Grade_Evaluation{ get; }
        [Required] public string Grade_Date { get; }

        public NetCreateGradRequest(string token, string subjectID, string superSubjectID, string grade_Name, string grade_Grade, string grade_Evaluation, string grade_Date) {
            Token = token;
            SubjectID = subjectID;
            SuperSubjectID = superSubjectID;
            Grade_Name = grade_Name;
            Grade_Grade = grade_Grade;
            Grade_Evaluation = grade_Evaluation;
            Grade_Date = grade_Date;
        }
    }
}
