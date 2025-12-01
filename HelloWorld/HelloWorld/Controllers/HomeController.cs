using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private List<String> _students;
    public HomeController()
    {
        _students = new List<String>()
        {
            "Mehmet",
            "Hakan",
            "Murat",
            "Selahattin",
            "Murat"
        };
    }
    [HttpGet]
    public IActionResult Greetings()
    {
        return Ok("Hello, World from HomeController."); // 200
    }

    [HttpGet("students")] // api/home/students
    public List<String> GetStudents() => _students;
    

    [HttpGet("students/{id}")] // api/home/students/{id}
    public IActionResult GetOneStudent(int id)
    {
        if (id <= 0 || id >= _students.Count)
        {
            return NotFound("Student not found"); // 404
        }
        return Ok(_students[id-1]);
    }
}
