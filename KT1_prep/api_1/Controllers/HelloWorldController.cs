using Microsoft.AspNetCore.Mvc;

namespace api_1.Controllers
{
    [Route("api/hw")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            return Ok(await Task.FromResult("Hello World"));
        }

        [HttpGet("{nr}")]
        public async Task<ActionResult<int>> EchoNumber([FromRoute] int nr)
        {
            return Ok(await Task.FromResult<int>(nr));
        }
    }
}
