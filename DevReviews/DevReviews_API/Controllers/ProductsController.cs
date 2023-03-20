using DevReviews_API.Entities;
using DevReviews_API.Models;
using DevReviews_API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Instaciando no construtor
        private readonly DevReviewsDbContext _DevReviewsDbContext;
        public ProductsController(DevReviewsDbContext devReviewsDbContext)
        {
            _DevReviewsDbContext = devReviewsDbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _DevReviewsDbContext.Products;
            return Ok(products);
        }
        //api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var products = _DevReviewsDbContext.Products.SingleOrDefault(p => p.Id == id);
            if(products == null)
            {
                return NotFound();
            }
            //Se não achar retona NOtFound
            return Ok();
        }
        //Criação - Cadastro de Objetos
        [HttpPost]
        public IActionResult Post(AddProductInputModel addProductInputModel)
        {
            var product = new Product(
                addProductInputModel.Title, 
                addProductInputModel.Description, 
                addProductInputModel.Price);

            _DevReviewsDbContext.Products.Add(product);

            return CreatedAtAction(nameof(GetById), new {id = product.Id}, addProductInputModel);
        }
        //Update do cadastro de Objetos
        //api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProductInputModel updateProductInputModel)
        {
            //Se houver erros de validação retornar BadRequest()
            //Se não existir o produto especificado retornar NotFound()

            var product = _DevReviewsDbContext.Products.SingleOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            product.Update(updateProductInputModel.Description, updateProductInputModel.Price);
            return NoContent();
        }

    }
}
