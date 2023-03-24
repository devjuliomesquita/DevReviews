using AutoMapper;
using DevReviews_API.Entities;
using DevReviews_API.Models;
using DevReviews_API.Persistence;
using DevReviews_API.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews_API.Controllers
{
    [Route("api/Products/{productId}/[controller]")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public ProductReviewsController(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        // GET url: api/products/{idProduct}/pruductreviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int productId,  int id)
        {
            var productReview = await _repository.GetReviewByIdAsync(id);
            if (productReview == null)
            {
                return NotFound();
            }
            var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReview);

            return Ok(productDetails);
        }
        // POST url: api/products/{idProduct}/pruductreviews
        [HttpPost]
        public async Task<IActionResult> Post(int productId, AddProductReviewsInputModel model)
        {
            var productReview = new ProductReviews(model.Author, model.Comments, model.Rating, productId);

            await _repository.AddReviewAsync(productReview);

            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId }, model);
        }
    }
}
