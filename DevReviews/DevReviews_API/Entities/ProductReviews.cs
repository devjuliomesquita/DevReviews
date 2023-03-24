namespace DevReviews_API.Entities
{
    public class ProductReviews
    {
        public ProductReviews(string author, string comments, int rating, int productId)
        {
            Author = author;
            Comments = comments;
            Rating = rating;
            RegisteredAt = DateTime.Now;
            ProductId = productId;
        }

        public int Id { get; private set; }
        public string Author { get; private set; }
        public string Comments { get; private set; }
        public int Rating { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public int ProductId { get; private set; }
    }
}
