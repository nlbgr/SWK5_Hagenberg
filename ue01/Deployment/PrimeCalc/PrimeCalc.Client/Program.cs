// See https://aka.ms/new-console-template for more information

using PrimeCalc;
using Newtonsoft.Json;

const int DEFAULT_LIMIT = 50;

static void PrintPrimesAsJson(int limit) {
    var primes = new List<int>();

    for (int i = 0; i <= limit; ++i) {
        if (PrimeChecker.IsPrime(i)) {
            primes.Add(i);
        }
    }

    string json = JsonConvert.SerializeObject(
        new {
            Count = primes.Count(),
            Primes = primes
        }
    );

    Console.WriteLine(json);
}

int limit = args.Length == 1 ? int.Parse(args[0]) : DEFAULT_LIMIT;
PrintPrimesAsJson(limit);
