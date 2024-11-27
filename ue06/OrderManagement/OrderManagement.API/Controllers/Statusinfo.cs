using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.API.Controllers;

public static class Statusinfo
{
    public static ProblemDetails CustomerAlreadyExist(Guid customerId) => 
        new ProblemDetails { Title = "Conflicting Customer IDs", Detail = $"Customer with ID {customerId} already exists" };

    public static ProblemDetails InvalidCustomerId(Guid customerId) =>
        new ProblemDetails { Title = "Invalid Customer IDs", Detail = $"Customer with ID {customerId} does not exist" };
}
