using Microsoft.AspNetCore.Mvc;

namespace BeneNotenrechner.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class DemoDataController : ControllerBase {

        [HttpGet]
        public ContentResult Get() {
            return Content(DBManager.instance.GetData());
        }
    }


}
