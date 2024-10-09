using CarManagementService.Domain.Abstractions.Specifications;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagementService.Infrastructure.Repositories.Evaluators;

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
        if (!orderModels.Any())
        {
            return query
                .Skip(specification.Skip)
                .Take(specification.Take);
        }
        
        var orderedQuery = AddFirstOrderExpression(query, orderModels.First());
        foreach (var orderModel in orderModels.Skip(1))
        {
            orderedQuery = AddAnotherOrderExpression(orderedQuery, orderModel);
        }

        return orderedQuery
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