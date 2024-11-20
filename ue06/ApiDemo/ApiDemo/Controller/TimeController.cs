using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controller;

[Route("time2")]
public class TimeController : ControllerBase
{
    [HttpGet]
    public object Get() {
        // return DateTime.UtcNow.ToString(@"o");
        // return Content(DateTime.UtcNow.ToString(@"o"), "text/plain");
        return new { Time = DateTime.UtcNow.ToString(@"o") };
    }
}
