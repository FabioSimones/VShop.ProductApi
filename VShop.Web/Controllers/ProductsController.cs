﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Roles;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            
            var result = await _productService.GetAllProducts(await GetAccessToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        

        [HttpGet]
        public async Task<ActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM, await GetAccessToken());

                if(result != null)
                    return RedirectToAction(nameof(Index));
                
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");
            }
            return View(productVM);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

            var result = await _productService.FindProductById(id, await GetAccessToken());

            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productVM, await GetAccessToken());

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
        {
            var result = await _productService.FindProductById(id, await GetAccessToken());

            if(result is null)
                return View("Error");
            return View(result);
        }

        [HttpPost(), ActionName("DeleteProduct")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id, await GetAccessToken());

            if(!result)
                return View("Error");

            return RedirectToAction("Index");
        }

        private async Task<string> GetAccessToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}
