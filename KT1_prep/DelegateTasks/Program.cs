
// (2)
int Multiply(int a, int b) => a * b;

// (3)
MathDelegate md = Multiply;

// (4)
Console.Out.WriteLine(md.Invoke(2, 3));
Console.Out.WriteLine();

// (5)
List<MathDelegate> mds = new List<MathDelegate>();
mds.Add(Multiply);
mds.Add((a, b) => a + b);

// (6)
foreach (var m in mds)
{
    Console.Out.WriteLine(m.Invoke(4,5));
}
Console.Out.WriteLine();


// ########### Delegate anlegen der int gibt und 2 ints nimmt
public delegate int MathDelegate(int a, int b);

// ########### Methode implementieren die Deleate erfüllt (Multiplikation)
// siehe (2)

// ########### Variable vom type des Delegates erstellen und die Funktion aus 2 zuweisen
// siehe (3)

// ########### Funktion über die Variable aufrufen
// siehe (4)

// ########### Liste vom Typen des Gelegates anlegen und Multiplikation und Addition einfügen
// siehe (5)

// ########### mit Schleife alle Delegates auf 2 Zahlen anwenden
// siehe (6)