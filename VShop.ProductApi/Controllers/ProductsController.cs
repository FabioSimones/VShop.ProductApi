﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDTO = await _productService.GetProducts();
            if (productsDTO == null)
                return NotFound("Products not found");

            return Ok(productsDTO);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var productsDTO = await _productService.GetProductById(id);
            if (productsDTO == null)
                return NotFound("Product not found");

            return Ok(productsDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest("Invalid Data");

            await _productService.AddProduct(productDTO);

            return new CreatedAtRouteResult("GetProduct", new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
                return BadRequest();
            if (productDTO == null)
                return BadRequest();

            await _productService.UpdateProduct(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var productDTO = await _productService.GetProductById(id);
            if (productDTO == null)
                return NotFound("Category not found.");

            await _productService.RemoveProduct(id);
            return Ok(productDTO);
        }
    }
}