using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;
using ModelApp.Models;

namespace ModelApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet] // api/books
    public IActionResult GetAllBooks()
    {
        var list = ApplicationContextInMemory.Books;
        if (list.Count.Equals(0))
            return NoContent(); // 204
        return Ok(list);
    }

    [HttpGet("{id:int}")] // api/books/{id}
    public IActionResult GetOneBook([FromRoute(Name ="id")] int id)
    {
        // Eski kod yapısı : Prosedürel / Yapılandırılmış programlama paradigması
        //foreach (Book book in ApplicationContextInMemory.Books)
        //{
        //    if (book.Id == id)
        //        return Ok(book); // 200
        //}
        //return NotFound(); // 404

        var book = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound(); // 404
        return Ok(book); // 200
    }

    [HttpPost]
    public IActionResult CreateOneBook([FromBody] Book book)
    {
        try
        {
            int max = 0;
            foreach (Book b in ApplicationContextInMemory.Books)
            {
                if (b.Id > max)
                    max = b.Id;
            }

            //var max = ApplicationContextInMemory
            //    .Books
            //    .Max(b => b.Id);

            book.Id = max + 1;
            ApplicationContextInMemory.Books.Add(book);
            return StatusCode(201, book); // 201
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // 400
        }
    }
}