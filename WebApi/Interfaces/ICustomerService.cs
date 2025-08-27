using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseResult<Customer>> CreateCustomerAsync(CustomerRequest customer);
        Task<CustomerResponseResult<IEnumerable<Customer>>> GetAllCustomersAsync();
        Task<CustomerResponseResult<Customer>> GetCustomerByIdAsync(string customerId);
    }
}