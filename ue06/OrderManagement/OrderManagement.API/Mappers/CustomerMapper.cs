using OrderManagement.API.DTOs;
using OrderManagement.Domain;
using Riok.Mapperly.Abstractions;

namespace OrderManagement.API.Mappers;

[Mapper]
public static partial class CustomerMapper
{
    public static partial CustomerDto ToCustomerDto(this Customer customer);
    public static partial IEnumerable<CustomerDto> ToCustomerDtoEnumeration(this IEnumerable<Customer> customers);
    public static partial Customer ToCustomer(this CustomerDto customerDto);

    [MapperIgnoreTarget(nameof(Customer.TotalRevenue))] // ist dazu da um den Fehler zu unterdrücken weil das alles okay ist
    public static partial Customer ToCustomer(this CustomerForCreationDto customerDto);

    [MapperIgnoreTarget(nameof(Customer.Id))]
    [MapperIgnoreTarget(nameof(Customer.TotalRevenue))]
    public static partial void UpdateCustomer(this CustomerForUpdateDto customerDto, Customer customer);
}
