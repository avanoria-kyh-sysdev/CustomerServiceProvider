using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;


    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerRequest customerRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var customer = await _customerService.CreateCustomerAsync(customerRequest);
        if (!customer.Succeeded)
            return StatusCode(customer.StatusCode, customer.Error);

        return StatusCode(customer.StatusCode, customer.Result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        
        if (!customers.Succeeded)
            return StatusCode(customers.StatusCode, customers.Error);
        
        return StatusCode(customers.StatusCode, customers.Result);
    }


    // GET api/customers/{customerId}
    // GET api/customers?customerId=123

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(string customerId)
    {
        var customer = await _customerService.GetCustomerByIdAsync(customerId);
        
        if (!customer.Succeeded)
            return StatusCode(customer.StatusCode, customer.Error);
        
        return StatusCode(customer.StatusCode, customer.Result);
    }
}
