using System;
using AutoMapper;
using CatalogService.Dtos;


namespace CatalogService.Repositories;

    public class MappingProfile : Profile
    {
    public MappingProfile()
    {
        CreateMap<ProductDto, Product>();
    }
    }
