using System.Linq.Expressions;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;

namespace CarManagementService.Domain.Abstractions.Specifications;

public interface ISpecification<TEntity>
    where TEntity : BaseEntity
{
    int Skip { get; }
    int Take { get; }
    
    IEnumerable<Expression<Func<TEntity, object>>> GetIncludes();
    IEnumerable<OrderModel<TEntity>> GetOrderModels();
    Expression<Func<TEntity, bool>> SatisfiedBy();
}