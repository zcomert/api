using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForRegistrationDto 
{
    public String? TCKN { get; init; }
    public String? FirstName { get; init; }
    public String? LastName { get; init; }
    
    [Required(ErrorMessage ="Kullanıcı adı zorunlu bir alandır!")]
    public String UserName { get; init; }

    [Required(ErrorMessage ="Parola zorunlu bir alandır!")]
    public String Password { get; init; }
    
    public String? Email { get; init; }
    public String? PhoneNumber { get; init; }
    public ICollection<string> Roles { get; init; } = new HashSet<string>();
}
