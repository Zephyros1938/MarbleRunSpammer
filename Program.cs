namespace Zephyros1938.Marblerun;

public class Program
{
    private static HttpClient MarblerunMainClient = new()
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
    };
    public static async Task Main(String[] args)
    {
        if (args.Length != 1) throw new ArgumentException("Must specify repetition count.");
        StringToInt(args[0], out Int32 repetitions);
        for(int i = 0; i < repetitions; i++)
        {
            Console.WriteLine(i);
        }
        await GetAsync(MarblerunMainClient);
    }

    static async Task GetAsync(HttpClient client)
    {
        using HttpResponseMessage response = await MarblerunMainClient.GetAsync("todos/3");

        response.EnsureSuccessStatusCode();
        Console.WriteLine(response.RequestMessage);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}");
    }

    public static void StringToInt(String InString, out Int32 OutInt)
    {
        bool result = Int32.TryParse(InString, out Int32 number);
        if (result) { OutInt = number; return; };
        throw new InvalidCastException($"Could not cast {InString} To Int");
    }
}