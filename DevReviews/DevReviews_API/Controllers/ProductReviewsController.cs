using AutoMapper;
using DevReviews_API.Models;
using DevReviews_API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews_API.Controllers
{
    [Route("api/Products/{productId}/[controller]")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly DevReviewsDbContext _devReviewsDbContext;
        private readonly IMapper _mapper;
        public ProductReviewsController(DevReviewsDbContext devReviewsDbContext, IMapper mapper)
        {
            _devReviewsDbContext = devReviewsDbContext;
            _mapper = mapper;
        }
        // GET url: api/products/{idProduct}/pruductreviews/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int productId,  int id)
        {
            var productReviews = _devReviewsDbContext.ProductReviews.SingleOrDefault(p => p.Id == id);
            if(productReviews == null)
            {
                return NotFound();
            }
            var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReviews);
            //Se não existir o Id especiicado retornar NotFound();
            return Ok(productDetails);
        }
        // POST url: api/products/{idProduct}/pruductreviews
        [HttpPost]
        public IActionResult Post(int productId, AddProductReviewsInputModel AddProductReviewsInputModel)
        {
            //Se estiver com os dados inválidos retornar o badRequest()
            return CreatedAtAction(nameof(GetById), new {id = 1, productId = 2}, AddProductReviewsInputModel);
        }
    }
}
