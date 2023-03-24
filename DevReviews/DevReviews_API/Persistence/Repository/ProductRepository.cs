using DevReviews_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevReviews_API.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DevReviewsDbContext _dbContext;

        public ProductRepository(DevReviewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddReviewAsync(ProductReviews productReviews)
        {
            await _dbContext.ProductReviews.AddAsync(productReviews);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> GetDetailsByIdAsync(int id)
        {
            return await _dbContext
                .Products
                .Include(p => p.Reviews)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductReviews> GetReviewByIdAsync(int id)
        {
            return await _dbContext.ProductReviews.SingleOrDefault(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        Task<ProductReviews> IProductRepository.GetReviewByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
