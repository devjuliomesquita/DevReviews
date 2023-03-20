using DevReviews_API.Entities;

namespace DevReviews_API.Persistence
{
    public class DevReviewsDbContext
    {
        public DevReviewsDbContext()
        {
            Products = new List<Product>();
        }
        public List<Product> Products { get; set; }
    }
}
