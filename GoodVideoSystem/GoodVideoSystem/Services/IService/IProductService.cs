using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Services.IService
{
    public interface IProductService
    {
        IEnumerable<Product> getProducts(int page_id, int pageSize, out int recordcount);
        IEnumerable<Product> getProducts(out int recordcount);
        Product getProduct(int id);
        void addProduct(Product product);
        void editProduct(Product p);
        void deleteProduct(Product p);
    }
}
