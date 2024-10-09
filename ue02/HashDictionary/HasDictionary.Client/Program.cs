// See https://aka.ms/new-console-template for more information

using HashDictionary.Impl;

static IDictionary<string, int> TestIndexerAndAdd()
{
    var cityInfo = new HashDictinary<string, int>();

    try
    {
        cityInfo["Hagenberg"] = 2_500;
        cityInfo["Linz"] = 200_000;
        cityInfo["Linz"] = 250_000;
        cityInfo.Add("Wien", 2_000_000);
        cityInfo.Add("Wien", 2_000_000);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
    }

    try
    {
        Console.WriteLine($"dict[\"Hagenberg\"] = {cityInfo["Hagenberg"]}");
        Console.WriteLine($"dict[\"Linz\"] = {cityInfo["Linz"]}");
        Console.WriteLine($"dict[\"Wien\"] = {cityInfo["Wien"]}");
        Console.WriteLine($"dict[\"Graz\"] = {cityInfo["Graz"]}"); // KeyNotFoundException
    }
    catch (KeyNotFoundException e)
    {
        Console.WriteLine($"{e.GetType().Name}: {e.Message}");
    }
    
    return cityInfo;
}

static void PrintDictionary<K, V>(IDictionary<K, V> dict)
{
    foreach (KeyValuePair<K, V> pair in dict)
    {
        Console.WriteLine($"{pair.Key} -> {pair.Value}");
    }
}

var cityInfo = TestIndexerAndAdd();
PrintDictionary(cityInfo);
