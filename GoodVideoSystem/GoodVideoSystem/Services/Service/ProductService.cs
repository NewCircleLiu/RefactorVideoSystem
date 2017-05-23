using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class ProductService : BaseService, IProductService
    {
        private IProductRepository productRepository { get; set; }
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.AddDisposableObject(productRepository);
        }

        public IEnumerable<Product> getProducts()
        {
            return productRepository.getProducts();
        }
    }
}