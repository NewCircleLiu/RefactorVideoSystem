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

        public IEnumerable<Product> getProducts(int page_id,int pageSize,out int recordcount)
        {
            return productRepository.getProducts(page_id,pageSize,out recordcount);
        }
        public IEnumerable<Product> getProducts(out int recordcount)
        {
            return productRepository.getProducts(out recordcount);
        }
        public Product getProduct(int id)
        {
            return productRepository.getProduct(id);
        }
        public void addProduct(Product product)
        {
            productRepository.addProduct(product);
        }
        public void editProduct(Product p)
        {
            productRepository.editProduct(p);
        }
        public void deleteProduct(Product p)
        {
            productRepository.deleteProduct(p);
        }
    }
}