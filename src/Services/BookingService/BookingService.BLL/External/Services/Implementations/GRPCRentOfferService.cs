using System.Net.Security;
using AutoMapper;
using BookingService.BLL.External.Services.Interfaces;
using BookingService.BLL.Models.Options;
using BookingService.BLL.Models.Results;
using Contracts.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GRPCUserServiceClient = Contracts.Protos.GRPCRentOfferService.GRPCRentOfferServiceClient;
    
namespace BookingService.BLL.External.Services.Implementations;

public class GRPCRentOfferService : IRentOfferService
{
    private readonly string _grpcServerAddress;
    private readonly IMapper _mapper;
    private readonly ILogger<GRPCRentOfferService> _logger;

    public GRPCRentOfferService(
        IOptions<GRPCOptions> options,
        IMapper mapper,
        ILogger<GRPCRentOfferService> logger)
    {
        _grpcServerAddress = options.Value.ConnectionStringRentOffer;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RentOfferResult?> GetRentOfferById(Guid id, CancellationToken cancellationToken = default)
    {
        using var channel = GrpcChannel.ForAddress(_grpcServerAddress);
        
        _logger.LogInformation("[gRPC Client] Sending GetRentOfferById request for rentOfferId: {RentOfferId}", id);
        
        var client = new GRPCUserServiceClient(channel);
        
        var request = new RentOfferRequest() { RentOfferId = id.ToString() };
        
        var response = await client.GetRentOfferByIdAsync(request, cancellationToken: cancellationToken);
        
        if (string.IsNullOrEmpty(response.Id))
        {
            _logger.LogWarning("[gRPC Client] Received empty response for rentOfferId: {RentOfferId}", id);
            
            return null;
        }
        
        _logger.LogInformation("[gRPC Client] Received GetRentOfferById response for rentOfferId: {RentOfferId}", id);
        
        return _mapper.Map<RentOfferResult>(response);
    }
}