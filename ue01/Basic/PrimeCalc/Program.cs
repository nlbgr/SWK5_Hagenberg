// See https://aka.ms/new-console-template for more information

using PrimeCalc;

const int MAX = 50;
int n = 0;

for (int i = 2; i <= MAX; ++i) {
    if(PrimeChecker.IsPrime(i)) {
        Console.WriteLine($"{i} is prime");
        ++n;
    }
}

Console.WriteLine($"Number of prime numbers in [2,{MAX}] = {n}");