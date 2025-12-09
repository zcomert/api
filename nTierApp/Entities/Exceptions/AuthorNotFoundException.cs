namespace Entities.Exceptions;

public class AuthorNotFoundException : NotFoundException
{
    public AuthorNotFoundException(int id) 
        : base($"Yazar Id:{id} bulunamadı!")
    {
    }
}
