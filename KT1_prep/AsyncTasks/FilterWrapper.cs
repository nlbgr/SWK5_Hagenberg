public static class FilterWrapper
{
    public static async Task<IEnumerable<T>> FilterAsync<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        var result = new List<T>();
        foreach (var item in items)
        {
            await Task.Delay(100);
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static bool IsPrime(int n) => n >= 2 && !Enumerable.Range(2, (int)Math.Sqrt(n + 1) - 1).Any(i => n % i == 0);
}

