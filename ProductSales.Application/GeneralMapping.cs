using AutoMapper;
using ProductSales.Application.Dtos;
using ProductSales.Domain.Models;

namespace ProductSales.Application
{
    internal class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
