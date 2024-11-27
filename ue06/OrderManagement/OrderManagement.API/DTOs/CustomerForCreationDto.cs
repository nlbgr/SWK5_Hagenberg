using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using OrderManagement.Domain;

namespace OrderManagement.API.DTOs;


// Domainentypen sind nicht der Platz für Serialisierungen und so, weil die technologie unabhängig sind
public record CustomerForCreationDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    [Range(1000, 9999, ErrorMessage = "Zip Code must have 4 digits")] // Validierung
    public required int ZipCode { get; set; }

    public required string City { get; set; }

    public required Rating Rating { get; set; }
}
