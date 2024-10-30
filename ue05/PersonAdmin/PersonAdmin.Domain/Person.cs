namespace PersonAdmin.Domain;

public class Person(int id, string firstName, string lastName, DateTime dateOfBirth) {
    public int Id { get; set; } = id;
    public string FirstName { get; set; } = firstName ?? throw new ArgumentNullException(nameof(firstName));
    public string LastName { get; set; } = lastName ?? throw new ArgumentNullException(nameof(lastName));
    public DateTime DateOfBirth { get; set; } = dateOfBirth;

    public override string ToString() {
        return $"Person(id: {Id}, FirstName: {FirstName}, LastName: {LastName}, DateOfBirth: {DateOfBirth:yyyy-MM-dd}";
    }
}