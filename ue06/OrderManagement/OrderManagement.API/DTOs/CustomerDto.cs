using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using OrderManagement.Domain;

namespace OrderManagement.API.DTOs;


// Domainentypen sind nicht der Platz für Serialisierungen und so, weil die technologie unabhängig sind
public record CustomerDto
{
    //[JsonRequired] //  optional because of 'required' keyowrd
    public required Guid Id { get; set; }

    //[JsonRequired] //  optional because of 'required' keyowrd
    public required string Name { get; set; }

    //[JsonRequired] //  optional because of 'required' keyowrd
    public required int ZipCode { get; set; }

    //[JsonRequired] //  optional because of 'required' keyowrd
    //[JsonPropertyName("location")] // city zu location umbenennen
    public required string City { get; set; }

    //[JsonRequired] //  optional because of 'required' keyowrd
    //[JsonConverter(typeof(JsonStringEnumConverter))] // konvertiert die enum Werte (also 0, 1, 2) auf die wirklichen Namen als string nur für dieses Element
    public required Rating Rating { get; set; }

    //[JsonIgnore] // wird bei serialisierung ignoriert
    public decimal TotalRevenue { get; set; }
}
