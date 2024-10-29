using System.Linq.Expressions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Domain.Specifications.Common;

public abstract class IncludeSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    private readonly List<Expression<Func<TEntity, object>>> _includes = new();

    public IncludeSpecification(Expression<Func<TEntity, object>> includeExpression)
    {
        AddInclude(includeExpression);
    }

    protected IncludeSpecification<TEntity> AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        _includes.Add(includeExpression);
        return this;
    }

    public override IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
    {
        return _includes;
    }

    public override IEnumerable<OrderModel<TEntity>> GetOrderModels()
    {
        return new List<OrderModel<TEntity>>();
    }

    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        return entity => true;
    }
}