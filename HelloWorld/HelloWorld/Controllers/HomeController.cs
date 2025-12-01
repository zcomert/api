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

    [HttpGet("students")] // api/home/students
    public List<String> GetStudents()
    {
        var students =  new List<String>()
        {
            "Mehmet",
            "Hakan",
            "Murat",
            "Selahattin",
            "Murat"
        };
        return students;
    }

    [HttpGet("students/{id}")] // api/home/students/{id}
    public String GetOneStudent(int id)
    {
        var students = new List<String>()
        {
            "Mehmet",
            "Hakan",
            "Murat",
            "Selahattin",
            "Murat"
        };
        return students[id];
    }
}
