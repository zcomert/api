namespace Entities.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(int id) 
        : base($"Kategori Id:{id} bulunamadı!")
    {
    }
}
