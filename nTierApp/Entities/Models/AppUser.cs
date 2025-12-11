using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models;

public class AppUser : IdentityUser
{
    [Display(Name = "TC Kimlik No")]
    public String? TCKN { get; set; }
    
    [Display(Name = "İsim")]
    public String? FirstName { get; set; }
    
    [Display(Name = "Soyisim")]
    public String? LastName { get; set; }
}
