using System.Linq.Expressions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Domain.Specifications.Common;

public abstract class PaginationSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    public PaginationSpecification(int pageNumber, int pageSize)
    {
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
    }

    public override IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
    {
        return new List<Expression<Func<TEntity, object>>>();
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