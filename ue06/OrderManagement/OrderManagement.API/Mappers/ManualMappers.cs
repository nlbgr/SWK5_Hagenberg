using OrderManagement.API.DTOs;
using OrderManagement.Domain;

namespace OrderManagement.API.Mappers;

/* Commented out because of Automapper Mappersly
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

    public static Customer ToCustomer(this CustomerDto c)
    {
        return new Domain.Customer(
            c.Id,
            c.Name,
            c.ZipCode,
            c.City,
            c.Rating
        );
    }
}
*/
