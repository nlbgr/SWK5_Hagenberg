using System;

namespace PrimeCalc {
    public class PrimeChecker {
        const double EPS = 0.1;

        public static bool IsPrime(int number) {
            for (int i = 2; i <= Math.Sqrt(number) + EPS; ++i) {
                if (number % i == 0)
                    return false;
            }
            return true;
        }
    }
}