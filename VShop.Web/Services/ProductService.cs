﻿using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndPoint = "/api/product";
        private readonly JsonSerializerOptions _options;
        private ProductViewModel _productViewModel;
        private IEnumerable<ProductViewModel> _products;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> FindProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
