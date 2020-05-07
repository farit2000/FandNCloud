using Microsoft.AspNetCore.Mvc;

namespace FandNCloud.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello world!");
    }
}