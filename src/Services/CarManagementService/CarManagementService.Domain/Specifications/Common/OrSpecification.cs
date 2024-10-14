using System.Linq.Expressions;
using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;
using CarManagementService.Domain.Helpers;

namespace CarManagementService.Domain.Specifications.Common;

public sealed class OrSpecification<TEntity> : BaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    private readonly ISpecification<TEntity> _leftSpecification;
    private readonly ISpecification<TEntity> _rightSpecification;

    public OrSpecification(ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
    {
        _leftSpecification = leftSpecification;
        _rightSpecification = rightSpecification;
    }

    public override IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
    {
        var leftIncludes = _leftSpecification.GetIncludes();
        var rightIncludes = _rightSpecification.GetIncludes();

        return leftIncludes.Union(rightIncludes);
    }

    public override IEnumerable<OrderModel<TEntity>> GetOrderModels()
    {
        var leftOrderModels = _leftSpecification.GetOrderModels();
        var rightOrderModels = _rightSpecification.GetOrderModels();

        return leftOrderModels.Union(rightOrderModels);
    }

    public override Expression<Func<TEntity, bool>> SatisfiedBy()
    {
        var leftExpression = _leftSpecification.SatisfiedBy();
        var rightExpression = _rightSpecification.SatisfiedBy();
        
        return leftExpression.Or(rightExpression);
    }
}