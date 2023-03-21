using AutoMapper;
using DevReviews_API.Entities;
using DevReviews_API.Models;

namespace DevReviews_API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductReviews, ProductReviewViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailsViewModel>();
        }
    }
}
