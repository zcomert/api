using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models;

public class AppUser : IdentityUser
{
    public String? TCKN { get; set; }
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
}
