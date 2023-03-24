using DevReviews_API.Entities;

namespace DevReviews_API.Persistence.Repository
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetDetailsByIdAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task UpdateAsync(Product product);

        Task<ProductReviews> GetReviewByIdAsync(int id);
        Task AddReviewAsync(ProductReviews productReview);
    }
}
