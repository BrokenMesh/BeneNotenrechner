using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeneNotenrechner.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DemoDataController : ControllerBase {

        private readonly ILogger<DemoDataController> _logger;
        public DemoDataController(ILogger<DemoDataController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public ContentResult Get() {
            return Content(DBManager.instance.GetData());
        }
    }


}
