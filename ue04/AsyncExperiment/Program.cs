class Program
{
    static async Task Main(string[] args)
    {
        var result = await AsyncMethod();
        Console.WriteLine(result);
    }

    static async Task<string> AsyncMethod()
    {
        return await Task.FromResult("Hello World");
    }
}