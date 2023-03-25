using AutoMapper;
using DevReviews_API.Entities;
using DevReviews_API.Models;
using DevReviews_API.Persistence;
using DevReviews_API.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DevReviews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //Instaciando no construtor
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();

            //Sem AutoMapper
            //var productViewModel = products.Select(p => new ProductViewModel(p.Id, p.Title, p.Price));

            //Com AutoMapper
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel);
        }
        //api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetDetailsByIdAsync(id);
            if (product == null)
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
            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);
            return Ok(productDetails);
        }
        //Criação - Cadastro de Objetos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Description, model.Price);
            Log.Information("Método POST chamado!");

            await _repository.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }
        //Update do cadastro de Objetos
        //api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductInputModel model)
        {
            //Se houver erros de validação retornar BadRequest()
            //Se não existir o produto especificado retornar NotFound()

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Update(model.Description, model.Price);

            await _repository.UpdateAsync(product);

            return NoContent();
        }

    }
}
