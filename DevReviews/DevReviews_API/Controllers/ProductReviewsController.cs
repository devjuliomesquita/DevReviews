using DevReviews_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews_API.Controllers
{
    [Route("api/Products/{productId}/[controller]")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        // GET url: api/products/{idProduct}/pruductreviews/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int productId,  int id)
        {
            //Se não existir o Id especiicado retornar NotFound();
            return Ok();
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
