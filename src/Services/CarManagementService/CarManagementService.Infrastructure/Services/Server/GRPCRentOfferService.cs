using AutoMapper;
using CarManagementService.Domain.Repositories;
using CarManagementService.Domain.Specifications.RentOffer;
using Contracts.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using GRPCRentOfferServiceBase = Contracts.Protos.GRPCRentOfferService.GRPCRentOfferServiceBase;

namespace CarManagementService.Infrastructure.Services.Server;

public class GRPCRentOfferService : GRPCRentOfferServiceBase
{
    private readonly IRentOfferRepository _rentOfferRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GRPCRentOfferService> _logger;

    public GRPCRentOfferService(
        IRentOfferRepository rentOfferRepository,
        IMapper mapper,
        ILogger<GRPCRentOfferService> logger)
    {
        _rentOfferRepository = rentOfferRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GetRentOfferByIdResponse?> GetRentOfferById(RentOfferRequest request, ServerCallContext context)
    {
        _logger.LogInformation("[gRPC] GetRentOfferById called with rentOfferId: {RentOfferId}", request.RentOfferId);

        if (!Guid.TryParse(request.RentOfferId, out var rentOfferId))
        {
            _logger.LogWarning("[gRPC] Invalid rentOfferId format: {RentOfferId}", request.RentOfferId);
            
            return null;
        }
        
        var spec = new RentOfferByIdSpecification(rentOfferId);
    
        var rentOffer = await _rentOfferRepository.FirstOrDefault(spec, context.CancellationToken);

        return _mapper.Map<GetRentOfferByIdResponse>(rentOffer);
    }
}