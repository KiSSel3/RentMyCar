using AutoMapper;
using BookingService.BLL.External.Services.Interfaces;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Models.Results;
using Contracts.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GRPCUserServiceClient = Contracts.Protos.GRPCUserService.GRPCUserServiceClient;

namespace BookingService.BLL.External.Services.Implementations;

public class GRPCUserService : IUserService
{
    private readonly string _grpcServerAddress;
    private readonly IMapper _mapper;
    private readonly ILogger<GRPCUserService> _logger;
    
    public GRPCUserService(
        IOptions<GRPCOptions> options,
        IMapper mapper,
        ILogger<GRPCUserService> logger)
    {
        _grpcServerAddress = options.Value.ConnectionStringUser;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<UserResult> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var channel = GrpcChannel.ForAddress(_grpcServerAddress);

        _logger.LogInformation("[gRPC Client] Sending GetUserById request for userId: {UserId}", id);
        
        var client = new GRPCUserServiceClient(channel);

        var request = new UserRequest() { UserId = id.ToString() };

        var response = await client.GetUserByIdAsync(request, cancellationToken: cancellationToken);
        
        _logger.LogInformation("[gRPC Client] Received GetUserById response for userId: {UserId}", id);

        return _mapper.Map<UserResult>(response);
    }

    public async Task<bool> IsUserValidAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var channel = GrpcChannel.ForAddress(_grpcServerAddress);

        _logger.LogInformation("[gRPC Client] Sending IsUserValid request for userId: {UserId}", id);
        
        var client = new GRPCUserServiceClient(channel);

        var request = new UserRequest() { UserId = id.ToString() };

        var response = await client.IsUserValidAsync(request, cancellationToken: cancellationToken);

        _logger.LogInformation("[gRPC Client] Received IsUserValid response for userId: {UserId}, isValid: {IsValid}", 
            id, response.IsValid);
        
        return response.IsValid;
    }
}