using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Brand.Commands.CreateBrand;
using CarManagementService.Application.UseCases.Brand.Commands.UpdateBrand;
using CarManagementService.Domain.Data.Entities;
using CarManagementService.Presentation.Models.DTOs.Brand;

namespace CarManagementService.Presentation.Infrastructure.Mappers;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandRequestDTO, CreateBrandCommand>();
        CreateMap<BrandRequestDTO, UpdateBrandCommand>();
    }
}