using Microsoft.EntityFrameworkCore;
using PaymentGateway.Infrastructure.Data.Entities;
using System.Linq.Expressions;

namespace PaymentGateway.Infrastructure.Data;

public class PaymentRepository : IRepository<Payment>
{
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<Payment>().Add(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<Payment>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Payment?> FirstOrDefaultAsync(Expression<Func<Payment, bool>> predicate, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Payment>().Where(predicate).FirstOrDefaultAsync(cancellationToken);


    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Payment>().FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);

    public async Task UpdateAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        _dbContext?.Set<Payment>().Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
