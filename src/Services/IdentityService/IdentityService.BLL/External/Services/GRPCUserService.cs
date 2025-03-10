using AutoMapper;
using Contracts.Protos;
using Grpc.Core;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GRPCUserServiceBase = Contracts.Protos.GRPCUserService.GRPCUserServiceBase;

namespace IdentityService.BLL.External.Services;

public class GRPCUserService : GRPCUserServiceBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;
    private readonly ILogger<GRPCUserService> _logger;
    
    public GRPCUserService(
        UserManager<UserEntity> userManager,
        IMapper mapper,
        ILogger<GRPCUserService> logger)
    {
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GetUserByIdResponse?> GetUserById(UserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[gRPC] GetUserById called with userId: {UserId}", request.UserId);

        if (!Guid.TryParse(request.UserId, out var userId))
        {
            _logger.LogWarning("[gRPC] Invalid userId format: {UserId}", request.UserId);
            
            return new GetUserByIdResponse();
        }
        
        var user = await _userManager.Users
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u=>u.Id == userId, context.CancellationToken);

        if (user is null)
        {
            _logger.LogWarning("[gRPC] User not found for userId: {UserId}", userId);
            
            return new GetUserByIdResponse();
        }
        
        return _mapper.Map<GetUserByIdResponse>(user);
    }

    public override async Task<IsUserValidResponse> IsUserValid(UserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[gRPC] IsUserValid called with userId: {UserId}", request.UserId);
        
        var user = await _userManager.FindByIdAsync(request.UserId);
        
        return new IsUserValidResponse() { IsValid = user is not null };
    }
}