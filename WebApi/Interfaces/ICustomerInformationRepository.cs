using System.Linq.Expressions;
using WebApi.Data.Entities;

namespace WebApi.Interfaces;

public interface ICustomerInformationRepository
{
    Task<bool> AddAsync(CustomerInformationEntity customerInformation);
    Task<bool> DeleteAsync(Expression<Func<CustomerInformationEntity, bool>> predicate);
    Task<IEnumerable<CustomerInformationEntity>> GetAllAsync(Expression<Func<CustomerInformationEntity, bool>>? predicate = null);
    Task<CustomerInformationEntity?> GetAsync(Expression<Func<CustomerInformationEntity, bool>> predicate);
    Task<bool> UpdateAsync(CustomerInformationEntity customer);
}