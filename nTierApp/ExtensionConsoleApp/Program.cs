namespace ExtensionConsoleApp;

public class Program
{
    static void Main(string[] args)
    {
        int number = 10;
        Console.WriteLine($"1'den {number} kadar " +
            $"olan sayıların toplamı = {number.SumToN("Ahmet")}");
    }
}
