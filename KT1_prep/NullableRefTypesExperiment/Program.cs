// The Nullable Operator "?" allows us to get more feedback from intellisense
// With the ?? Operator a simple "n if n != null else b" statement can be made with n ?? b
//      often used in classes to throw exception if an element is null
// With the Alvis Operator we can access Elements of an Object while checking if the object is not null
//      pseudocode "n.element if n != null else null" used as n!.element
// With the !. Operator (lovingly called "Trust me bro Operator") we can tell the compiler that the element is never null
//      used like this n!.element or said "trust me you can access element of n cause n is never null"


using System.Diagnostics.CodeAnalysis;

// Becuase of the Nullable Operator "?" we get feedback from intellisense
static IEnumerable<Person>? GetPersons()
{
    return null;
}

static Person? getPerson() => null;


// the type annotation gives extra information
static bool TryGetPerson(IEnumerable<Person>? persons, string name, [NotNullWhen(true)] out Person? person)
{
    if (persons is not null) // check needed cause we know this can be null
    {
        foreach (var p in persons)
        {
            if (p.Name == name)
            {
                person = p;
                return true;
            }
        }
    }

    person = null;
    return false;
}

// This operator returns the left result if it is not null else the right result
Person? p1 = getPerson() ?? new("Kain", 69);

// The Elvis Operator gives p1.Name if p1 is not null else it returns null and no error
Console.Out.WriteLine(p1?.Name);

// The "Trust me bro" Operator is used to dissable the check cause we know p1 is never null
Console.Out.WriteLine(p1!.Name);


Person? p2 = null; // Nullable cause p2 can be null if no person is found
var res = TryGetPerson(GetPersons(), "Adam", out p2);


record Person(string Name, int Age);