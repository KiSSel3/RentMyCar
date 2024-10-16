using AutoMapper;
using CarManagementService.Application.Models.DTOs;
using CarManagementService.Application.UseCases.Commands.Brand.CreateBrand;
using CarManagementService.Application.UseCases.Commands.Brand.UpdateBrand;
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