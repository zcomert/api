namespace Entities.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userName) 
        : base($"Kullanıcı Adı:'{userName}' bulunamadı!")
    {
    }
}
