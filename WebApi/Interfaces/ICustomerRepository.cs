using System.Linq.Expressions;
using WebApi.Data.Entities;

namespace WebApi.Interfaces;

public interface ICustomerRepository
{
    Task<bool> AddAsync(CustomerEntity customer);
    Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<IEnumerable<CustomerEntity>> GetAllAsync(Expression<Func<CustomerEntity, bool>>? predicate = null);
    Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<bool> UpdateAsync(CustomerEntity customer);
}