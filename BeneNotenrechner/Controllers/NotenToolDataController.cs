using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BeneNotenrechner.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class NotenToolDataController : ControllerBase {

        [HttpGet]
        public IActionResult Get(DataRequest _value) {


            return Ok();
        }

    }

    public class DataRequest {
        [Required] public string token { get; }
        [Required] public string profile_id { get; }
    }
}
