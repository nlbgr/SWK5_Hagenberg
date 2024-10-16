using System.Text.Json;
using PersonManagement;

PersonRepository personRepository = new PersonRepository();

try {
    using (var reader = new StreamReader("persons.json")){
        var json = reader.ReadToEnd();
        var persons = JsonSerializer.Deserialize<IEnumerable<Person>>(
            json, 
            new JsonSerializerOptions{PropertyNameCaseInsensitive = true}
        );

        if (persons is null){
            throw new Exception("Problem with deserialization of persons.json");
        }

        personRepository.AddPersons(persons);
    }

} catch (FileNotFoundException fnfEx){
    Console.WriteLine(fnfEx.Message);
    return;
}

// Here we can use the using declaration, because we need the object till the end of the method
using TextWriter textWriter = Console.Out;
//using TextWriter textWriter = new StreamWriter("result.txt");

textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person list");
textWriter.WriteLine("=====================================================");

personRepository.PrintPersons(textWriter);

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Persons in Hagenberg");
textWriter.WriteLine("=====================================================");

personRepository.FindPersonsByCity("Hagenberg").ForEach(p => textWriter.WriteLine(p));


textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person names");
textWriter.WriteLine("=====================================================");

// Variante 1
// foreach (var name in personRepository.GetPersonNames()){
//     textWriter.WriteLine($"{name.First} {name.Last}");
// }

// Variante 2: split Tuple directly
// foreach (var (first, last) in personRepository.GetPersonNames()) {
//     textWriter.WriteLine($"{first} {last}");
// }

// Variante 3:
personRepository.GetPersonNames().ForEach(p => textWriter.WriteLine($"{p.First} {p.Last}"));

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine($"Youngest person");
textWriter.WriteLine("=====================================================");

textWriter.WriteLine(personRepository.FindYoungestPerson());

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Persons sorted by age ascending");
textWriter.WriteLine("=====================================================");

personRepository.FindPersonsSortedByAgeAscending().ForEach(p => textWriter.WriteLine(p));
