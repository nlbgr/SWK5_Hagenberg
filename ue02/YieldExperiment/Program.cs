using System.Collections.Generic;

namespace YieldExample {
  class Program {
    public static IEnumerable<int> GetValues() {
      yield return 97;
      yield return 98;
      yield return 99;
    }

    static void Main(string[] args) {
      foreach (int i in GetValues()) {
        System.Console.WriteLine(i);
      }
    }
  }
}
