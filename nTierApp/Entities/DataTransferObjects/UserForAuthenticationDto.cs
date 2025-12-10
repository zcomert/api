namespace Entities.DataTransferObjects;

public record UserForAuthenticationDto
{
    public String UserName { get; init; }
    public String Password { get; init; }
}
