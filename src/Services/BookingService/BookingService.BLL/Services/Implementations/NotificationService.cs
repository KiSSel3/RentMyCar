using AutoMapper;
using BookingService.BLL.Exceptions;
using BookingService.BLL.Models.DTOs.Notification;
using BookingService.BLL.Providers.Interfaces;
using BookingService.BLL.Services.Interfaces;
using BookingService.DAL.Repositories.Interfaces;
using BookingService.Domain.Entities;

namespace BookingService.BLL.Services.Implementations;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserProvider _userProvider;
    private readonly IMapper _mapper;

    public NotificationService(
        INotificationRepository notificationRepository,
        IUserProvider userProvider,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _userProvider = userProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var notifications = await _notificationRepository.GetAllAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsAsync(CancellationToken cancellationToken = default)
    {
        var notifications = await _notificationRepository.GetUnsentNotificationsAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications); 
    }

    public async Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var isUserValid = await _userProvider.IsUserValidAsync(userId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }

        var notifications = await _notificationRepository.GetByUserIdAsync(userId, cancellationToken);

        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<IEnumerable<NotificationDTO>> GetUnsentNotificationsByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var isUserValid = await _userProvider.IsUserValidAsync(userId, cancellationToken);
        if (!isUserValid)
        {
            throw new EntityNotFoundException("UserEntity", userId);
        }
        
        var notifications = await _notificationRepository.GetUnsentNotificationsByUserIdAsync(userId, cancellationToken);
        
        return _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
    }

    public async Task<NotificationDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var notification = await _notificationRepository.GetByIdAsync(id, cancellationToken);
        if (notification is null)
        {
            throw new EntityNotFoundException(nameof(NotificationEntity), id);
        }
        
        return _mapper.Map<NotificationDTO>(notification);
    }
}