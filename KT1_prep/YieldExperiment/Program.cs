// Yield liefert den nächsten Wert in einem Iterator zurück
// Control Flow: Funktion läuft bis zu einem yield, gibt den Wert zurück, pausiert und merkt sich den Zustand
//               Anschließend wird der "Main" Code weiter ausgeführt, bis der nächste Wert des Iterators benötigt wird
//               Beim nächsten Aufruf wird die Funktion an der Stelle fortgesetzt, an der sie pausiert wurde    

// Beispiel Ausgage:

// ---Before Yield
//    0
//    some other load main
// --- After Yield
// --- Before Yield
//    1
//    some other load main
// --- After Yield
// --- Before Yield
//    2
//    some other load main
// --- After Yield
// --- Before Yield
//    3
//    some other load main
// --- After Yield
// --- Before Yield
//    4
//    some other load main
// --- After Yield

IEnumerable<int> yieldSomeInts()
{
    for (int i = 0; i < 5; ++i)
    {
        Console.Out.WriteLine("--- Before Yield");
        yield return i;
        Console.Out.WriteLine("--- After Yield");
    }
}

foreach (var i in yieldSomeInts())
{
    Console.WriteLine("   " + i);
    Console.WriteLine("   " + "some other load main");
}