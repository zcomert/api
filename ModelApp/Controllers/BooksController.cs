using Microsoft.AspNetCore.Mvc;
using ModelApp.Data;
using ModelApp.Models;

namespace ModelApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;

    public BooksController(ILogger<BooksController> logger)
    {
        _logger = logger;
    }

    [HttpGet] // api/books
    public IActionResult GetAllBooks()
    {
        var list = ApplicationContextInMemory.Books;
        if (list.Count.Equals(0))
        {
            _logger.LogWarning("Veri kaynaðýnda hiç kitap yok!");
            return NoContent(); // 204
        }
        
        _logger.LogInformation("{count} adet kitap bulundu.", list.Count);
        return Ok(list);
    }

    [HttpGet("{id:int}")] // api/books/{id}
    public IActionResult GetOneBook([FromRoute(Name ="id")] int id)
    {
        var book = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        
        if (book is null)
        {
            _logger.LogWarning("Id'si {id} olan kitap bulunamadý.", id);
            return NotFound(); // 404
        }
            
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

            book.Id = max + 1;
            ApplicationContextInMemory.Books.Add(book);
            _logger.LogInformation("Yeni kitap eklendi. Id: {id}", book.Id);
            return StatusCode(201, book); // 201
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Yeni kitap eklenirken bir hata oluþtu.");
            return BadRequest(ex.Message); // 400
        }
    }

    [HttpPut("{id:int}")] // api/books/{id}
    public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] Book book)
    {
        if (book is null)
        {
            _logger.LogWarning("Güncelleme için geçersiz kitap verisi alýndý.");
            return BadRequest(); // 400
        }
            

        var existing = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        
        if (existing is null)
        {
            _logger.LogWarning("Id'si {id} olan kitap bulunamadý.", id);
            return NotFound(); // 404
        }
            

        // If client supplied an Id in the body, ensure it matches the route id
        if (book.Id != 0 && book.Id != id)
        {
            _logger.LogWarning("Route id ve body id eþleþmiyor. Route id: {routeId}, Body id: {bodyId}", id, book.Id);
            return BadRequest("Route id and body id must match."); // 400
        }
           

        // Update allowed properties
        existing.Title = book.Title;
        existing.Price = book.Price;
        _logger.LogInformation("Id'si {id} olan kitap güncellendi.", id);
        return Ok(existing); // 200
    }

    [HttpDelete("{id:int}")] // api/books/{id}
    public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
    {
        var book = ApplicationContextInMemory
            .Books
            .FirstOrDefault(b => b.Id == id);
        if (book is null)
        {
            _logger.LogWarning("Id'si {id} olan kitap bulunamadý.", id);
            return NotFound(); // 404
        }
            
        ApplicationContextInMemory.Books.Remove(book);
        _logger.LogInformation("Id'si {id} olan kitap silindi.", id);
        return NoContent(); // 204
    }
}
