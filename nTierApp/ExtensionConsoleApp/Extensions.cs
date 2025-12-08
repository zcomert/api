namespace ExtensionConsoleApp;

public static class Extensions
{
    // 2. Kural : Extension metotlar static olmalıdır.
    // 3. Kural : Extension metotların ilk parametresi this ile işaretlenir
    // ve bu parametre extension metotun genişlettiği türü belirtir.
    public static int SumToN(this int n, string name)
    {
        if (n < 0)
            throw new ArgumentException("n must be non-negative");
        return n * (n + 1) / 2; // O(1) time complexity
    }
}
