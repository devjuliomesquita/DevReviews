using AutoMapper;
using DevReviews_API.Entities;
using DevReviews_API.Models;
using DevReviews_API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevReviews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Instaciando no construtor
        private readonly DevReviewsDbContext _DevReviewsDbContext;
        private readonly IMapper _IMapper;
        public ProductsController(DevReviewsDbContext devReviewsDbContext, IMapper iMapper)
        {
            _DevReviewsDbContext = devReviewsDbContext;
            _IMapper = iMapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _DevReviewsDbContext.Products;
            
            //Sem AutoMapper
            //var productViewModel = products.Select(p => new ProductViewModel(p.Id, p.Title, p.Price));

            //Com AutoMapper
            var productViewModel = _IMapper.Map<List<ProductViewModel>>(products);
            
            return Ok(productViewModel);
        }
        //api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var products = _DevReviewsDbContext
                .Products
                .Include(p => p.Reviews)
                .SingleOrDefault(p => p.Id == id);
            if(products == null)
            {
                return NotFound();
            }
            //Se não achar retona NOtFound

            //Sem AutoMapper
            //var reviewsViewModel = products
            //    .Reviews
            //    .Select(r => new ProductReviewViewModel(r.Id, r.Author, r.Rating, r.Comments, r.RegisteredAt))
            //    .ToList();
            //var productDetails = new ProductDetailsViewModel(
            //    products.Id,
            //    products.Title,
            //    products.Description,
            //    products.Price,
            //    products.RegisteredAt,
            //    reviewsViewModel
            //    );

            //Com AutoMapper
            var productDetails = _IMapper.Map<List<ProductDetailsViewModel>>(products);
            return Ok(productDetails);
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
            _DevReviewsDbContext.SaveChanges();

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
            _DevReviewsDbContext.Products.Update(product);
            _DevReviewsDbContext.SaveChanges();
            return NoContent();
        }

    }
}
