using System.Linq.Expressions;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;

namespace CarManagementService.Domain.Specifications.Common;

public abstract class SortSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    private readonly List<OrderModel<TEntity>> _orderModels = new();

    public SortSpecification(Expression<Func<TEntity, object>> orderExpression, bool isDescending = false)
    {
        AddOrderBy(orderExpression, isDescending);
    }

    protected SortSpecification<TEntity> AddOrderBy(Expression<Func<TEntity, object>> orderExpression, bool isDescending = false)
    {
        _orderModels.Add(new OrderModel<TEntity>(orderExpression, isDescending));
        return this;
    }

    public override IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
    {
        return new List<Expression<Func<TEntity, object>>>();
    }

    public override IEnumerable<OrderModel<TEntity>> GetOrderModels()
    {
        return _orderModels;
    }

    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        return entity => true;
    }
}