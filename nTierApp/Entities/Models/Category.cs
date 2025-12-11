using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Display(Name = "Kategori Adı")]
    public String CategoryName { get; set; } = String.Empty;
}
