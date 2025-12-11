using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForAuthenticationDto
{
    [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
    public String UserName { get; init; }
    
    [Required(ErrorMessage = "Parola zorunludur!")]
    public String Password { get; init; }
}
