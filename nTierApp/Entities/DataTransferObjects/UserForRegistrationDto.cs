using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public record UserForRegistrationDto 
{
    [Display(Name="TC Kimlik No")]
    public String? TCKN { get; init; }
    [Display(Name="İsim")]
    public String? FirstName { get; init; }
    [Display(Name="Soyisim")]
    public String? LastName { get; init; }
    
    [Required(ErrorMessage ="Kullanıcı adı zorunlu bir alandır!")]
    [Display(Name="Kullanıcı Adı")]
    public String UserName { get; init; }

    [Required(ErrorMessage ="Parola zorunlu bir alandır!")]
    [Display(Name="Parola")]
    public String Password { get; init; }
    
    [Display(Name="E-posta")]
    public String? Email { get; init; }

    [Display(Name="Telefon Numarası")]
    public String? PhoneNumber { get; init; }
    public ICollection<string> Roles { get; init; } = new HashSet<string>();
}
