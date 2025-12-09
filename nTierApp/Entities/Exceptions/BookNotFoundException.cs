namespace Entities.Exceptions;

public class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(int id) 
        : base($"Kitap Id:{id} bulunamadı!")
    {

    }
}
