using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.BackgroundServices;
using OrderManagement.Api.Controllers;
using OrderManagement.API.DTOs;
using OrderManagement.API.Mappers;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]                         // verbessert Dev experience (auto 400er Code bei Exceptin ; Binding von Complexen Typen aus dem Body)
    [ApiConventionType(typeof(WebApiConventions))] // legt für diesen Controller die Conventions aus der gegebenen Kalle fest
    public class CustomersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;
        private readonly UpdateChannel updateChannel;

        public CustomersController(IOrderManagementLogic logic, UpdateChannel updateChannel)
        {
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
            this.updateChannel = updateChannel ?? throw new ArgumentNullException(nameof(updateChannel));
        }

        [HttpGet]
        //[ApiConventionMethod(typeof(WebApiConventions), nameof(WebApiConventions.Get))] // legt die Conventions für diese Methode fest
        public async Task<IEnumerable<CustomerDto>> GetCustomers(Rating? rating)
        {
            // Mock für ersten Test
            // return new List<Customer>
            // {
            //     new(id: Guid.NewGuid(), name: "Franz Huber", zipCode: 4020, city: "Linz", rating: Rating.A)
            // };

            if (rating is null)
            {
                //return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
                return (await logic.GetCustomersAsync()).ToCustomerDtoEnumeration();
            }
            else
            {
                return (await logic.GetCustomersByRatingAsync(rating.Value)).Select(c => c.ToCustomerDto());
            }
        }

        [HttpGet("{customerId}")]
        /*[ProducesResponseType(StatusCodes.Status200OK)] // sind auskommentiert, weil eine API-Convention verwendet wird
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]*/
        public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId) // [FromRoute] ist das default Verhalten
        {
            var customer = (await logic.GetCustomerAsync(customerId));
            if (customer is null)
            {
                return NotFound(Statusinfo.InvalidCustomerId(customerId));
            }

            return customer.ToCustomerDto();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerForCreationDto customerDto)
        {
            if (customerDto.Id != Guid.Empty && await logic.CustomerExistsAsync(customerDto.Id))
            {
                return Conflict();
            }

            Domain.Customer customer = customerDto.ToCustomer();
            await logic.AddCustomerAsync(customer);

            return CreatedAtAction(
                actionName: nameof(GetCustomerById), 
                routeValues: new { customerId = customer.Id },
                value: customer.ToCustomerDto()
            );
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] CustomerForUpdateDto customerDto)
        {
            Customer? customer = await logic.GetCustomerAsync(customerId);
            if (customer is null)
            {
                return NotFound();
            }

            customerDto.UpdateCustomer(customer);
            await logic.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            if (!await logic.DeleteCustomerAsync(customerId))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{customerId}/update-totals")]
        public async Task<ActionResult> UpdateCustomerTotals([FromRoute] Guid customerId, CancellationToken cancellationToken)
        {
            if (!await logic.CustomerExistsAsync(customerId))
            {
                return NotFound(Statusinfo.InvalidCustomerId(customerId));
            }

            // Sync. implementation
            // await logic.UpdateTotalRevenueAsync(customerId);
            // return NoContent();

            // Async implementation
            //  * Processing ist handed over to hosted service
            //  * Service sends response immediately

            // If update channel is full, it produces backpressure
            // So if AddUpdateTaskAsync blocks, it cant accept further requestst
            await updateChannel.AddUpdateTaskAsync(customerId, cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                return BadRequest();
            }

            return Accepted();
        }
    }
}
