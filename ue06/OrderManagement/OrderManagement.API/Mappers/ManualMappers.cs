using OrderManagement.API.DTOs;
using OrderManagement.Domain;

namespace OrderManagement.API.Mappers;

public static class ManualMappers
{
    public static CustomerDto ToCustomerDto(this Customer c)
    {
        return new()
        {
            Id = c.Id, Name = c.Name, ZipCode = c.ZipCode, City = c.City, Rating = c.Rating,
            TotalRevenue = c.TotalRevenue
        };
    }
}
