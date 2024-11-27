using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using OrderManagement.Domain;

namespace OrderManagement.API.DTOs;


// Domainentypen sind nicht der Platz für Serialisierungen und so, weil die technologie unabhängig sind
public record CustomerForUpdateDto
{
    public required string Name { get; set; }

    public required int ZipCode { get; set; }

    public required string City { get; set; }

    public required Rating Rating { get; set; }
}
