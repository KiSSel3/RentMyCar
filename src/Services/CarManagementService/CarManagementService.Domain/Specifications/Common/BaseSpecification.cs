using System.Linq.Expressions;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Domain.Specifications.Common;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : BaseEntity
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = Int32.MaxValue;
    public abstract IEnumerable<Expression<Func<TEntity, object>>> GetIncludes();
    public abstract IEnumerable<OrderModel<TEntity>> GetOrderModels();

    public abstract Expression<Func<TEntity, bool>> SatisfiedBy();
    
    public ISpecification<TEntity> And(ISpecification<TEntity> other)
    {
        return new AndSpecification<TEntity>(this, other);
    }

    public ISpecification<TEntity> Or(ISpecification<TEntity> other)
    {
        return new OrSpecification<TEntity>(this, other);
    }
}