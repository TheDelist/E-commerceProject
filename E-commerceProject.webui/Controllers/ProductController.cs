using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerceProject.webui.Controllers
{
  
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

         //localhost/products/telefon?page=1
        
        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 1;
            ProductListViewModel productView;
            
            if (string.IsNullOrEmpty(category))
            { 
                productView = new ProductListViewModel()
                {
                    PageInfo=new PageInfo(){
                        TotalItems=_productService.Count(),
                        CurrentPage=page,
                        ItemPerPage=pageSize,
                        CurrentCategories=category,

                    },
                    ProductList = _productService.GetAll(page, pageSize)
                };
                 
            }
            else
            {
               
                productView = new ProductListViewModel()
                {
                    PageInfo=new PageInfo(){
                        TotalItems=_productService.GetCountByCategory(category),
                        CurrentPage=page,
                        ItemPerPage=pageSize,
                        CurrentCategories=category,

                    },
                    ProductList = _productService.GetProductByCategory(category, page, pageSize)
                };
            }

            return View(productView);
        }

        

        public IActionResult Details(string url){
              if (string.IsNullOrEmpty(url))
            {
                return NotFound();
            }
            var product = _productService.GetProductDetails(url);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

         public IActionResult Search (string q){
            
            ProductListViewModel productView; 
                productView = new ProductListViewModel()
                {
                    
                    ProductList = _productService.GetSearchResult(q)
                };

            return View(productView);
        }
    }
}