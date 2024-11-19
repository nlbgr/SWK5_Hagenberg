namespace LINQ.Data;

public class StudentRepository
{
    private IEnumerable<Student> students = new List<Student>
    {
        new("s12345", "Huber", "Se", new[] {2, 3, 2, 1, 3}),
        new("s12388", "Mayr", "MC", new[] {1, 2, 3, 2, 1}),
        new("s12321", "Bauer", "se", new[] {3, 5, 5, 2, 3}),
        new("s12353", "Schmidt", "SE", new[] {2, 4, 3, 2, 1}),
    };

    public IEnumerable<Student> getStudents() => students;
}