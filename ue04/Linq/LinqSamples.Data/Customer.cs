namespace LinqSamples.Data;

public record Customer
{
  public required string Name { get; set; }

  public required decimal Revenue { get; set; }

  public required int YearOfFoundation { get; set; }

  public required Address Location { get; set; }

  public required Rating Rating { get; set; }

  public override string ToString()
  {
    return $"{Name}, {Location.City} ({Location.Country}): {Revenue:N0} Euro, found. {YearOfFoundation}, Rating: {Rating}";
  }
}