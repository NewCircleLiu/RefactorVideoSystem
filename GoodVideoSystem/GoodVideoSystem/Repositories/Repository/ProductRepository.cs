using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BaseDbContext context) : base(context) { }

        public IEnumerable<Product> getProducts(int page_id, int pageSize, out int recordcount)
        {
            recordcount = this.DbSet.Count();
            return this.Get(p => true, page_id, pageSize, p => p.ModifyTime, true);
        }

        public IEnumerable<Product> getProducts(out int recordcount)
        {
            recordcount = this.DbSet.Count();
            return this.Get(p => true);
        }
        public Product getProduct(int id)
        {
            return this.Get(item => item.ProductId == id).FirstOrDefault();
        }
        public void addProduct(Product product)
        {
            this.Add(product);

        }
        public void editProduct(Product p)
        {
            this.Update(p);
        }
        public void deleteProduct(Product p)
        {
            this.Delete(p);
        }
    }
}