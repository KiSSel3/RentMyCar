using System.Linq.Expressions;
using CarManagementService.Domain.Entities;

namespace CarManagementService.Domain.Models;

public class OrderModel<TEntity>
    where TEntity : BaseEntity
{
    public OrderModel(Expression<Func<TEntity, object>> orderExpression, bool needOrderByDescending)
    {
        OrderExpression = orderExpression;
        NeedOrderByDescending = needOrderByDescending;
    }
    public Expression<Func<TEntity, object>> OrderExpression { get; }
    public bool NeedOrderByDescending { get; }
}