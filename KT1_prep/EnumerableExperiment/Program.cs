// Zweck: Experimente mit IEnumerable und IEnumerator
// Man sieht, dass IEnumerable und IEnumerator nicht direkt implementiert werden müssen
// Es reicht, wenn die Klasse die Methoden GetEnumerator und Current implementiert (also das Iteratormuster)
// Damit kann die Syntax `foreach` verwendet werden

DummyClass dummy = new DummyClass();

// Hier wird implizit GetEnumerator aufgerufen was einen Iterator liefert
// Danach wird mittels MoveNext und Current die Liste durchlaufen
// Man sieht, dass die Klassen nicht tatsächlich IEnumerable implementieren müssen
foreach (var item in dummy)
{
    Console.WriteLine(item);
}


// Klasse die nicht direkt IEnumerable implementiert
class DummyClass
{
    public List<string> List { get; } = ["a", "b", "c", "d", "e"];

    public DummyEnumerator GetEnumerator()
    {
        return new DummyEnumerator(this);
    }
}

// Klasse die IEnumerator nicht direkt implementiert
class DummyEnumerator (DummyClass entity)
{
    private DummyClass Entity { get; } = entity; 
    private int Idx { get; set; } = -1;
    private IList<string> Items { get { return Entity.List; } }

    public string? Current
    {
        get { return Idx >= 0 ? Items[Idx] : null; }
    }

    public bool MoveNext()
    {
        Idx++;
        return Idx < Items.Count;
    }
}