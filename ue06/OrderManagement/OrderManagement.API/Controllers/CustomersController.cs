using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs;
using OrderManagement.API.Mappers;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]                         // verbessert Dev experience (auto 400er Code bei Exceptin ; Binding von Complexen Typen aus dem Body)
    public class CustomersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;

        public CustomersController(IOrderManagementLogic logic)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomers(Rating? rating)
        {
            // Mock für ersten Test
            // return new List<Customer>
            // {
            //     new(id: Guid.NewGuid(), name: "Franz Huber", zipCode: 4020, city: "Linz", rating: Rating.A)
            // };

            if (rating is null)
            {
                return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
            }
            else
            {
                return (await logic.GetCustomersByRatingAsync(rating.Value)).Select(c => c.ToCustomerDto());
            }
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId) // [FromRoute] ist das Default verhalten
        {
            var customer = (await logic.GetCustomerAsync(customerId));
            if (customer is null)
            {
                return NotFound();
            }

            return customer.ToCustomerDto();
        }
    }
}
