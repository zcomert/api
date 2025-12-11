using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models;

public class Author
{
    public int AuthorId { get; set; }
    
    [Display(Name="İsim")]
    public String? FirstName { get; set; }

    [Display(Name="Soyisim")]
    public String? LastName { get; set; }
}
