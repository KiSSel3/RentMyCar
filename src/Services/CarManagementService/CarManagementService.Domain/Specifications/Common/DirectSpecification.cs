using System.Linq.Expressions;
using CarManagementService.Domain.Entities;
using CarManagementService.Domain.Models;

namespace CarManagementService.Domain.Specifications.Common;

public abstract class DirectSpecification<TEntity> : BaseSpecification<TEntity>
     where TEntity : BaseEntity
 {
     private readonly Expression<Func<TEntity, bool>> _predicate;
 
     protected DirectSpecification(Expression<Func<TEntity, bool>> predicate)
     {
         _predicate = predicate;
     }
     
     public override Expression<Func<TEntity, bool>> SatisfiedBy()
     {
         return _predicate;
     }
 
     public override IEnumerable<Expression<Func<TEntity, object>>> GetIncludes()
     {
         return new List<Expression<Func<TEntity, object>>>();
     }

     public override IEnumerable<OrderModel<TEntity>> GetOrderModels()
     {
         return new List<OrderModel<TEntity>>();
     }
 }