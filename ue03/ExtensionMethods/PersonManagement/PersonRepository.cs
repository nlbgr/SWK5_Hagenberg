using System.Linq;

namespace PersonManagement;

public class PersonRepository
{
  private readonly IList<Person> persons = new List<Person>();

  public void AddPerson(Person person) {
        persons.Add(person);
  }

  public void AddPersons(IEnumerable<Person> persons) {
      // Variante 1
      //foreach (var p in persons){
      //    this.persons.Add(p);
      //}

      // Variante 2
      // Implement extension method of IList
      this.persons.AddAll(persons);

      // without the this (so without extension class) it would look like this clunky shit
      //CollectionExtensions.AddAll(this.persons, persons);
  }

  public void PrintPersons(TextWriter textWriter) {
      // Variante 1
      //foreach (var p in persons){
      //    textWriter.WriteLine(p);
      //}

      // Variante 2
      // Implement Extension Method for IEnumerable
      persons.ForEach((Person p) => textWriter.WriteLine(p));

      // Magere Version von Variante 2:
      //Action<Person> printAction = delegate(Person p) {
      //    textWriter.WriteLine(p);
      //};
      //persons.ForEach(printAction);
  }

  public IEnumerable<(string? First, string? Last)> GetPersonNames() {
      // Variante 1
      // foreach (var p in persons){
      //     yield return (p.FirstName, p.LastName);
      // }

      // Variante 2
      return persons.Map(p => (p.FirstName, p.LastName));
  }

  public IEnumerable<Person> FindPersonsByCity(string city) {
      // Variante 1 (sehr sehr mager)
      //var personList = new List<Person>();
      //foreach (var p in persons){
      //    if (p.City.Equals(city)){
      //        personList.Add(p);
      //    }
      //}
      //
      //return personList;

      // Variante 2 (bissi besser)
      //foreach (var p in persons){
      //    if (p.City.Equals(city)){
      //        yield return p;
      //    }
      //}

      // Variante 3
      return persons.Filter((Person p) => p.City.Equals(city));
  }

  public Person FindYoungestPerson() {
      if (persons.Count <= 0){
          throw new ArgumentException("Minimum for empty collection undefined");
      }

      // Uses LINQ
      //return persons.MaxBy(p => p.DateOfBirth);

      // Uses selfWritten MaxByCustom (I like the LINQ stuff more, since its better to read)
      return persons.MaxByCustom((Person one, Person two) => one.DateOfBirth.CompareTo(two.DateOfBirth));
  }


  public IEnumerable<Person> FindPersonsSortedByAgeAscending() {
      // EZ with LINQ
      return persons.OrderByDescending(p => p.DateOfBirth);
  }
}
