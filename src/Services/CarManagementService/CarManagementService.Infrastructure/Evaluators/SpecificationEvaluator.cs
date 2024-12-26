using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Evaluators;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification)
        where TEntity : BaseEntity
    {
        var query = inputQuery;
        
        query = specification
            .GetIncludes()
            .Aggregate(query, (current, include) => current.Include(include));
        
        var whereExp = specification.SatisfiedBy();
        if (whereExp is not null)
        {
            query = query.Where(whereExp);
        }
        
        var orderModels = specification.GetOrderModels().ToList();
        if (orderModels.Any())
        {
            var orderedQuery = AddFirstOrderExpression(query, orderModels.First());
            
            query = orderModels.Skip(1)
                .Aggregate(orderedQuery, AddAnotherOrderExpression);
        }

        return query
            .Skip(specification.Skip)
            .Take(specification.Take);
    }

    private static IOrderedQueryable<TEntity> AddFirstOrderExpression<TEntity>(
        IQueryable<TEntity> query, 
        OrderModel<TEntity> orderModel)
        where TEntity : BaseEntity
    {
        return orderModel.NeedOrderByDescending
            ? query.OrderByDescending(orderModel.OrderExpression)
            : query.OrderBy(orderModel.OrderExpression);
    }

    private static IOrderedQueryable<TEntity> AddAnotherOrderExpression<TEntity>(
        IOrderedQueryable<TEntity> query,
        OrderModel<TEntity> orderModel)
        where TEntity : BaseEntity
    {
        return orderModel.NeedOrderByDescending
            ? query.ThenByDescending(orderModel.OrderExpression)
            : query.ThenBy(orderModel.OrderExpression);
    }
}