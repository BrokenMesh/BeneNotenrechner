﻿using BeneNotenrechner.Backend;
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
    public class NT_UpdateSuperSubjectController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(NetUpdateSuperSubjectRequest _request) {
            User? _user = UserManager.GetUserFromToken(_request.Token);
            if (_user == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve User!")));

            Profile? _profile = _user.GetProfile();
            if (_profile == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve Profile!")));

            SuperSubject? _superSubject = _profile.GetSuperSubject(_request.SuperSubjectID);
            if (_superSubject == null) return BadRequest(JsonSerializer.Serialize(new NetError("Could not resolve SuperSubject!")));

            _superSubject.Update(_request.Name);

            return Ok();
        }
    }

    public class NetUpdateSuperSubjectRequest
    {
        [Required] public string Token { get; }
        [Required] public string SuperSubjectID { get; }
        [Required] public string Name { get; }

        public NetUpdateSuperSubjectRequest(string token, string superSubjectID, string name) {
            Token = token;
            SuperSubjectID = superSubjectID;
            Name = name;
        }
    }
}
