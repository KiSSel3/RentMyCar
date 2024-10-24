using AutoMapper;
using BookingService.BLL.Exceptions;
using BookingService.BLL.Models.DTOs.Notification;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookingService.BLL.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserProvider _userProvider;
    private readonly ILogger<NotificationService> _logger;
    private readonly IMapper _mapper;
    
    public NotificationService(
        INotificationRepository notificationRepository,
        IUserProvider userProvider,
        ILogger<NotificationService> logger,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _userProvider = userProvider;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving all notifications");
        
        var notifications = await _notificationRepository.GetAllAsync(cancellationToken);
        
        _logger.LogInformation("Retrieved {Count} notifications", notifications.Count());
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving all unsent notifications");
        
        var notifications = await _notificationRepository.GetUnsentNotificationsAsync(cancellationToken);
        
        _logger.LogInformation("Retrieved {Count} unsent notifications", notifications.Count());
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications); 
    }

    public async Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving notifications for user {UserId}", userId);
        
        var isUserValid = await _userProvider.IsUserValidAsync(userId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }

        var notifications = await _notificationRepository.GetByUserIdAsync(userId, cancellationToken);

        _logger.LogInformation("Retrieved {Count} notifications for user {UserId}", 
            notifications.Count(), userId);
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving unsent notifications for user {UserId}", userId);
        
        var isUserValid = await _userProvider.IsUserValidAsync(userId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }
        
        var notifications = await _notificationRepository.GetUnsentNotificationsByUserIdAsync(userId, cancellationToken);
        
        _logger.LogInformation("Retrieved {Count} unsent notifications for user {UserId}", 
            notifications.Count(), userId);
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<NotificationDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving notification {NotificationId}", id);
        
        var notification = await _notificationRepository.GetByIdAsync(id, cancellationToken);
        if (notification is null)
        {
            throw new EntityNotFoundException(nameof(NotificationEntity), id);
        }
        
        _logger.LogInformation("Successfully retrieved notification {NotificationId}", id);
        
        return _mapper.Map<NotificationDTO>(notification);
    }
}