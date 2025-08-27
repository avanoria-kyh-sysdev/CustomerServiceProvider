using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Data.Contexts;
using WebApi.Data.Entities;
using WebApi.Interfaces;

namespace WebApi.Data.Repositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> AddAsync(CustomerEntity customer)
    {
        _context.Add(customer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(predicate);
        if (entity == null) return false;

        _context.Customers.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync(Expression<Func<CustomerEntity, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _context.Customers.ToListAsync();

        return await _context.Customers.Where(predicate).ToListAsync();
    }

    public async Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(predicate);
        return entity;
    }

    public async Task<bool> UpdateAsync(CustomerEntity customer)
    {
        var entity = await _context.Customers.FindAsync(customer);
        if (entity == null) return false;

        _context.Entry(entity).CurrentValues.SetValues(entity);
        _context.Customers.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
