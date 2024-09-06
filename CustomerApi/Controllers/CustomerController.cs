using ApplicationTier.Classes;
using ApplicationTier.Interfaces;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerMethods _customerMethods;

        public CustomerController(ICustomerMethods customerMethods)
        {
            _customerMethods = customerMethods;

        }

        [HttpPost(Name = "AddCustomer")]
        public async Task<JsonResult> AddCustomer(string firstName, string lastName)
        {

            var customer = await _customerMethods.AddCustomer(firstName, lastName, DateTime.Now.AddYears(-20));

            return new JsonResult(customer); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            // Call the delete method in the service layer
            var deletedCustomer = await _customerMethods.DeleteCustomer(id);

            // If customer doesn't exist, return NotFound (404)
            if (deletedCustomer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            // Return Ok (200) with the details of the deleted customer
            return Ok(deletedCustomer);
        }


    }
}
