namespace PersonManagement;

// public class Person
// {
// 	public uint Id { get; set; }
// 	public string? FirstName { get; set; }
// 	public string? LastName { get; set; }
// 	public DateTime DateOfBirth { get; set; }
// 	public string? City { get; set; }
//
// 	public override string ToString()
// 	{
// 	  return $"{Id} \t {FirstName} {LastName} \t {DateOfBirth.ToShortDateString()} \t {City}";
// 	}
// }

// Since Members of the Class Person where never changed, a record is a better choice
public record Person (uint Id, string? FirstName, string? LastName, DateTime DateOfBirth, string? City) {
    public override string ToString() {
	  return $"{Id} \t {FirstName} {LastName} \t {DateOfBirth.ToShortDateString()} \t {City}";
	}
}