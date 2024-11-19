
// 2
Console.WriteLine(5.IsEven());
Console.WriteLine(2.IsEven());
Console.Out.WriteLine();

// 3
foreach (var n in Enumerable.Range(-5, 10))
{
    Console.Out.WriteLine(n.Holds(n => n > 0));
}
Console.Out.WriteLine();

// 4
Console.WriteLine(Enumerable.Range(-5, 10).HoldsForAll(n => n > 0));
Console.WriteLine(Enumerable.Range(1, 10).HoldsForAll(n => n > 0));



public static class ExtensionWrapper
{
    // ########### Extension Methode IsEven für int schreiben
    public static bool IsEven(this int number) => number % 2 == 0;

    // ########### Einfaches Programm zur Demonstration vom vorherigen Beispiel
    // siehe oben (2)


    // ########### Delegate für ein Prädikat schreiben das einen int nimmt
    public delegate bool Predicate(int number);


    // ########### Extension Method Holds schreiben die das Prädikat verwendet
    public static bool Holds(this int number, Predicate pred) => pred.Invoke(number);


    // ########### holds und die Extension Method so verwenden dass alle positiven Zahlen ausgegeben werden
    // siehe oben (3)

    // ########### Das Prädikat allgemeiner definieren
    public delegate bool PredicatGeneric<T>(T number);


    // ########### Neue Erweiterungsmethode HoldsForAll definieren das auf IEnumerable<T> angewendet werden kann
    public static bool HoldsForAll<T>(this IEnumerable<T> collection, PredicatGeneric<T> pred) =>
        collection.All(item => pred.Invoke(item));


    // ########### zeigen wie Holds for all funktioniert
    // siehe oben (4)
}