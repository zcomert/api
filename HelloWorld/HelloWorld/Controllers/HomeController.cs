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
    public String Greetings()
    {
        return "Hello, World from HomeController.";
    }

    [HttpGet("students")] // api/home/students
    public List<String> GetStudents() => _students;
    

    [HttpGet("students/{id}")] // api/home/students/{id}
    public String GetOneStudent(int id)
    {
        if (id <= 0 || id >= _students.Count)
        {
            return "Student not found.";
        }
        return _students[id-1];
    }
}
