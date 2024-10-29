using System.Linq.Expressions;

namespace CarManagementService.Domain.Helpers;

public static class ExpressionHelper
{
    public static Expression<T> And<T>(this Expression<T> left, Expression<T> right)
    {
        var parameter = left.Parameters[0];

        if (ReferenceEquals(parameter, right.Parameters[0]))
        {
            return Expression.Lambda<T>(Expression.AndAlso(left.Body, right.Body), parameter);
        }

        return Expression.Lambda<T>(
            Expression.AndAlso(left.Body, Expression.Invoke(right, parameter)),
            parameter);
    }

    public static Expression<T> Or<T>(this Expression<T> left, Expression<T> right)
    {
        var parameter = left.Parameters[0];

        if (ReferenceEquals(parameter, right.Parameters[0]))
        {
            return Expression.Lambda<T>(Expression.OrElse(left.Body, right.Body), parameter);
        }

        return Expression.Lambda<T>(
            Expression.OrElse(left.Body, Expression.Invoke(right, parameter)),
            parameter);
    }
}