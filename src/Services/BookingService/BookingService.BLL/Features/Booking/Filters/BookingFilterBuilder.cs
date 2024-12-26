using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using MongoDB.Driver;

namespace BookingService.BLL.Features.Booking.Filters;

public class BookingFilterBuilder
{
    private readonly FilterDefinitionBuilder<BookingEntity> _builder = Builders<BookingEntity>.Filter;
    private readonly List<FilterDefinition<BookingEntity>> _filters = new();

    public BookingFilterBuilder ByUserId(Guid? userId)
    {
        if (userId.HasValue)
        {
            _filters.Add(_builder.Eq(b => b.UserId, userId.Value));
        }
        
        return this;
    }

    public BookingFilterBuilder ByRentOfferId(Guid? rentOfferId)
    {
        if (rentOfferId.HasValue)
        {
            _filters.Add(_builder.Eq(b => b.RentOfferId, rentOfferId.Value));
        }
        
        return this;
    }

    public BookingFilterBuilder ByStartDateFrom(DateTime? startDate)
    {
        if (startDate.HasValue)
        {
            _filters.Add(_builder.Gte(b => b.RentalStart, startDate.Value));
        }

        return this;
    }

    public BookingFilterBuilder ByStartDateTo(DateTime? startDate)
    {
        if (startDate.HasValue)
        {
            _filters.Add(_builder.Lte(b => b.RentalStart, startDate.Value));
        }

        return this;
    }

    public BookingFilterBuilder ByEndDateFrom(DateTime? endDate)
    {
        if (endDate.HasValue)
        {
            _filters.Add(_builder.Gte(b => b.RentalEnd, endDate.Value));
        }

        return this;
    }

    public BookingFilterBuilder ByEndDateTo(DateTime? endDate)
    {
        if (endDate.HasValue)
        {
            _filters.Add(_builder.Lte(b => b.RentalEnd, endDate.Value));
        }

        return this;
    }

    public BookingFilterBuilder ByStatus(BookingStatus? status)
    {
        if (status.HasValue)
        {
            _filters.Add(
                _builder.Where(b => b.Events.Any() && b.Events.Last().Status == status.Value)
            );
        }

        return this;
    }


    public BookingFilterBuilder ByDateOverlap(DateTime start, DateTime end)
    {
        _filters.Add(_builder.And(
            _builder.Lte(b => b.RentalStart, end),
            _builder.Gte(b => b.RentalEnd, start)));
        
        return this;
    }

    public FilterDefinition<BookingEntity> Build()
    {
        if (!_filters.Any())
        {
            return _builder.Empty;
        }

        return _builder.And(_filters);
    }
}