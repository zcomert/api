using Entities.ErrorModel;

namespace ExtensionConsoleApp;

public class Program
{
    static void Main(string[] args)
    {
        List<int> set1 = new() { 1, 2, 3, 4, 5 };  // tüm roller
        List<int> set2 = new() { 2, 4, 5 };

        set2.Where(x => set1.Contains(x))
            .ToList()
            .ForEach(x => Console.WriteLine(x));

        //var resultSet = set1.Intersect(set2);

        //foreach (var item in resultSet)
        //{
        //    Console.WriteLine(item);
        //}

        Console.ReadKey();

        static void ExtensionMetotSample()
        {
            int number = 10;
            Console.WriteLine($"1'den {number} kadar " +
                $"olan sayıların toplamı = {number.SumToN("Ahmet")}");

            var error = new ErrorDetails()
            {
                StatusCode = 404,
                Message = "Sayfa bulunamadı"
            };
            Console.WriteLine(error);
        }

    }
}
