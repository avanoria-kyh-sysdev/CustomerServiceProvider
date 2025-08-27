using WebApi.Data.Entities;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services;

public class CustomerService(ICustomerRepository customerRepository, ICustomerInformationRepository customerInformationRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ICustomerInformationRepository _customerInformationRepository = customerInformationRepository;

    public async Task<CustomerResponseResult<Customer>> CreateCustomerAsync(CustomerRequest customer)
    {
        if (customer == null)
            return new CustomerResponseResult<Customer>
            {
                Succeeded = false,
                StatusCode = 400,
                Error = "Customer data is null"
            };

        var exists = await _customerRepository.GetAsync(c => c.CustomerName == customer.CustomerName);
        if (exists != null)
            return new CustomerResponseResult<Customer>
            {
                Succeeded = false,
                StatusCode = 409,
                Error = "Customer with the same name already exists"
            };

        try
        {
            if (await _customerRepository.AddAsync(
                new CustomerEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerName = customer.CustomerName,
                })
            )
            {
                var customerEntity = await _customerRepository.GetAsync(c => c.CustomerName == customer.CustomerName);
                if (customerEntity != null)
                {
                    var result = await _customerInformationRepository.AddAsync(new CustomerInformationEntity
                    {
                        CustomerId = customerEntity.Id,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                    });

                    if (!result)
                    {
                        return new CustomerResponseResult<Customer>
                        {
                            Succeeded = true,
                            StatusCode = 201,
                            Error = $"An error occurred while creating the customer. No customer information was saved."
                        };
                    }

                    return new CustomerResponseResult<Customer>
                    {
                        Succeeded = true,
                        StatusCode = 201
                    };
                }
            }

        }
        catch (Exception ex)
        {
            return new CustomerResponseResult<Customer>
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"An error occurred while creating the customer: {ex.Message}"
            };
        }

        return new CustomerResponseResult<Customer>
        {
            Succeeded = false,
            StatusCode = 500,
            Error = $"An error occurred while creating the customer"
        };
    }

    public async Task<CustomerResponseResult<Customer>> GetCustomerByIdAsync(string customerId)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            return new CustomerResponseResult<Customer>
            {
                Succeeded = false,
                StatusCode = 400,
                Error = "Customer ID is null or empty"
            };
        try
        {
            var customerEntity = await _customerRepository.GetAsync(c => c.Id == customerId);
            if (customerEntity == null)
                return new CustomerResponseResult<Customer>
                {
                    Succeeded = false,
                    StatusCode = 404,
                    Error = "Customer not found"
                };
            var customerInfoEntity = await _customerInformationRepository.GetAsync(ci => ci.CustomerId == customerId);
            var customer = new Customer
            {
                Id = customerEntity.Id,
                CustomerName = customerEntity.CustomerName,
                Email = customerInfoEntity!.Email,
                PhoneNumber = customerInfoEntity?.PhoneNumber
            };
            return new CustomerResponseResult<Customer>
            {
                Succeeded = true,
                StatusCode = 200,
                Result = customer
            };
        }
        catch (Exception ex)
        {
            return new CustomerResponseResult<Customer>
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"An error occurred while retrieving the customer: {ex.Message}"
            };
        }
    }

    public async Task<CustomerResponseResult<IEnumerable<Customer>>> GetAllCustomersAsync()
    {
        try
        {
            var customerEntities = await _customerRepository.GetAllAsync();
            var customers = new List<Customer>();
            foreach (var customerEntity in customerEntities)
            {
                var customerInfoEntity = await _customerInformationRepository.GetAsync(ci => ci.CustomerId == customerEntity.Id);
                customers.Add(new Customer
                {
                    Id = customerEntity.Id,
                    CustomerName = customerEntity.CustomerName,
                    Email = customerInfoEntity!.Email,
                    PhoneNumber = customerInfoEntity?.PhoneNumber
                });
            }
            return new CustomerResponseResult<IEnumerable<Customer>>
            {
                Succeeded = true,
                StatusCode = 200,
                Result = customers
            };
        }
        catch (Exception ex)
        {
            return new CustomerResponseResult<IEnumerable<Customer>>
            {
                Succeeded = false,
                StatusCode = 500,
                Error = $"An error occurred while retrieving customers: {ex.Message}"
            };
        }
    }
}
