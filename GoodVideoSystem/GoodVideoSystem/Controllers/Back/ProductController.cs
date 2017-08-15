using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Models.Abstract;

namespace GoodVideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        private IProductService productService;
        private IPaging ip;

        public ProductController(IProductService productService, IPaging ip)
        {
            this.productService = productService;
            this.ip = ip;
        }

        public ActionResult Index(int page_id = 1)
        {
            int recordcount;
            IEnumerable<Product> productList = productService.getProducts(out recordcount);
            ip.GetCurrentPageData(productList, page_id);
            TempData["productCount"] = recordcount;
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/Product/Index/Page";
            ViewBag.account = manager.Account;
            return View(ip);

        }
        //添加产品页面
        public ActionResult AddProductPage()
        {
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;
            return View();
        }
        //添加产品
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productService.addProduct(product);
                TempData["info"] = "添加成功";
                return RedirectToAction("AddProductPage", "Product");
            }
            TempData["info"] = "添加失败";
            return RedirectToAction("AddProductPage", "Product");
        }

        //返回编辑产品页面
        public ActionResult EditProductPage(int productID)
        {
            Product p = productService.getProduct(productID);
            return View(p);
        }

        //编辑产品
        [HttpPost]
        public ActionResult EditProduct(Product p)
        {
            Product oldProduct = productService.getProduct(p.ProductId);

            if (oldProduct.Img != p.Img)
            {
                //删除原来的图片
                string imgUrl = Server.MapPath(oldProduct.Img);
                FileInfo img = new FileInfo(imgUrl);
                img.Delete();
            }

            oldProduct.Name = p.Name;
            oldProduct.Price = p.Price;
            oldProduct.Url = p.Url;
            oldProduct.Img = p.Img;
            productService.editProduct(oldProduct);

            if (ModelState.IsValid)
            {
                return Content("编辑成功");
            }
            return Content("编辑失败");
        }

        //删除产品
        public ActionResult DeleteProduct(int productID)
        {
            Product p = productService.getProduct(productID);
            if (ModelState.IsValid)
            {
                productService.deleteProduct(p);
                //删除视频对应的图片
                string imgUrl = Server.MapPath(p.Img);
                FileInfo img = new FileInfo(imgUrl);
                img.Delete();
                return Content("sucess");
            }
            return Content("failure");
        }
    }
}
