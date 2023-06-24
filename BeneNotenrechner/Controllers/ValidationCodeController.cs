using BeneNotenrechner.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeneNotenrechner.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValidationCodeController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post(NetValidationCode _netValidationCode) {

            string _error = UserManager.ValidateAndCreateUser(_netValidationCode.ValidationCode);

            return Ok(new NetError(_error));
        }

    }

    public class NetValidationCode
    {
        [Required] public string ValidationCode { get; }

        public NetValidationCode(string validationCode) {
            ValidationCode = validationCode;
        }
    }
}
