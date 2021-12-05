using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
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

        public IActionResult Index()
        {
            return View();
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
    }
}