using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public String Greetings()
    {
        return "Hello, World from HomeController.";
    }
}
