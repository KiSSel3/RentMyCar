using System.Linq.Expressions;
using BookingService.BLL.Infrastructure.Helpers;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;

namespace BookingService.BLL.Infrastructure.Filters;

public class BookingFilterBuilder
{
    private readonly List<Expression<Func<BookingEntity, bool>>> _filters = new();

    public BookingFilterBuilder ByUserId(Guid? userId)
    {
        if (userId.HasValue)
        {
            _filters.Add(b => b.UserId == userId.Value);
        }
        
        return this;
    }

    public BookingFilterBuilder ByRentOfferId(Guid? rentOfferId)
    {
        if (rentOfferId.HasValue)
        {
            _filters.Add(b => b.RentOfferId == rentOfferId.Value);
        }
        
        return this;
    }

    public BookingFilterBuilder ByStartDate(DateTime? startDate)
    {
        if (startDate.HasValue)
        {
            _filters.Add(b=>b.RentalStart >= startDate.Value);
        }

        return this;
    }
    
    public BookingFilterBuilder ByEndDate(DateTime? endDate)
    {
        if (endDate.HasValue)
        {
            _filters.Add(b=>b.RentalEnd <= endDate.Value);
        }

        return this;
    }

    public BookingFilterBuilder ByStatus(BookingStatus? status)
    {
        if (status.HasValue)
        {
            _filters.Add(b => b.Events
                .OrderByDescending(e => e.Timestamp)
                .First().Status == status.Value);
        }

        return this;
    }
    
    public BookingFilterBuilder ByDateOverlap(DateTime start, DateTime end)
    {
        _filters.Add(b => 
            (b.RentalStart <= end) && (b.RentalEnd >= start));
            
        return this;
    }
    
    public Expression<Func<BookingEntity, bool>> Build()
    {
        if (!_filters.Any())
        {
            return filter => true;
        }
        
        var combinedExpression = _filters.First();
        foreach (var expression in _filters.Skip(1))
        {
            combinedExpression = combinedExpression.And(expression);
        }

        return combinedExpression;
    }
}
