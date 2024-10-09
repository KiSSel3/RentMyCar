using System.Linq.Expressions;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;

namespace CarManagementService.Domain.Abstractions;

public interface ISpecification<TEntity>
    where TEntity : BaseEntity
{
    int Skip { get; }
    int Take { get; }
    
    IEnumerable<Expression<Func<TEntity, object>>> GetIncludes();
    IEnumerable<OrderModel<TEntity>> GetOrderModels();
    Expression<Func<TEntity, bool>> SatisfiedBy();
}