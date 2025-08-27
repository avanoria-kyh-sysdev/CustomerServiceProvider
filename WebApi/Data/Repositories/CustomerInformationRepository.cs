using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Data.Contexts;
using WebApi.Data.Entities;
using WebApi.Interfaces;

namespace WebApi.Data.Repositories;

public class CustomerInformationRepository(DataContext context) : ICustomerInformationRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> AddAsync(CustomerInformationEntity customerInformation)
    {
        _context.Add(customerInformation);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Expression<Func<CustomerInformationEntity, bool>> predicate)
    {
        var entity = await _context.CustomerInformation.FirstOrDefaultAsync(predicate);
        if (entity == null) return false;

        _context.CustomerInformation.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<CustomerInformationEntity>> GetAllAsync(Expression<Func<CustomerInformationEntity, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _context.CustomerInformation.ToListAsync();

        return await _context.CustomerInformation.Where(predicate).ToListAsync();
    }

    public async Task<CustomerInformationEntity?> GetAsync(Expression<Func<CustomerInformationEntity, bool>> predicate)
    {
        var entity = await _context.CustomerInformation.FirstOrDefaultAsync(predicate);
        return entity;
    }

    public async Task<bool> UpdateAsync(CustomerInformationEntity customer)
    {
        var entity = await _context.CustomerInformation.FindAsync(customer);
        if (entity == null) return false;

        _context.Entry(entity).CurrentValues.SetValues(entity);
        _context.CustomerInformation.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
