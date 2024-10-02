// See https://aka.ms/new-console-template for more information

using PrimeCalc;
using Newtonsoft.Json;

const int MAX = 50;

static void TestPrimeChecker() {
    int n = 0;

    for (int i = 2; i <= MAX; ++i) {
        if(PrimeChecker.IsPrime(i)) {
            Console.WriteLine($"{i} is prime");
            ++n;
        }
    }

    Console.WriteLine($"Number of prime numbers in [2,{MAX}] = {n}");
}

static void PrintPrimesAsJson() {
    var primes = new List<int>();

    for (int i = 0; i <= MAX; ++i) {
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

TestPrimeChecker();
PrintPrimesAsJson();
