using CarManagementService.Domain.Abstractions.Services;
using CarManagementService.Infrastructure.Options;
using Contracts.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GRPCUserServiceClient = Contracts.Protos.GRPCUserService.GRPCUserServiceClient;

namespace CarManagementService.Infrastructure.Services.Client;

public class GRPCUserService : IUserService
{
    private readonly string _grpcServerAddress;
    private readonly ILogger<GRPCUserService> _logger;

    public GRPCUserService(IOptions<GRPCOptions> options, ILogger<GRPCUserService> logger)
    {
        _grpcServerAddress = options.Value.ConnectionString;
        _logger = logger;
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