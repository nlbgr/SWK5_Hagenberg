//#nullable enable
//enables nullable only for this file

using System.Diagnostics.CodeAnalysis;

static void Basics() {
    var Person = new Person("Huber", "Franz");
    Person.FirstName = null;
    Person.LastName = null; // warning
    Person.LastName = "Huber-Mayr";

    var firstUpper = Person.FirstName.ToUpper(); // Warning -> NulReferenceException
    var lastName = Person.LastName.ToUpper(); // no warning, since its not annotated as nullable
    firstUpper = Person?.FirstName?.ToUpper(); // no warning, since we do null checks with elvis-operator
}




// Migration ----------------------------------------------

static IEnumerable<Person>? GetPersons() {
    return null; // may return null
}

static void PrintPersons(IEnumerable<Person> persons) {
    foreach (var p in persons) {
        Console.WriteLine(p);
    }
}

static void Migration() {
    IEnumerable<Person>? persons = GetPersons();
    PrintPersons(persons ?? []);
}

// ========================================================

static bool TryGetPerson(IEnumerable<Person>? persons, string lastName, [NotNullWhen(true)] out Person? person) {
    if (persons is not null){
        foreach (var p in persons){
            if (p.LastName.Equals(lastName)){
                person = p;
                return true;
            }
        }
    }

    person = null;
    return false;
}

static void Attributes() {
    var person = new Person("Huber", "Franz");
    person.Address = null; // warning with [DisallowNull]

    IEnumerable<Person>? persons = GetPersons();
    if (TryGetPerson(persons, "Huber", out Person? p)){
        Console.WriteLine(p.LastName);
    }
}

Migration();
Attributes();
Basics();