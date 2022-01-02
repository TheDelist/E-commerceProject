using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_commerceProject.webui.Models;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Concrete.MSSQL;

namespace E_commerceProject.webui.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        //localhost:5000/home/index
        public IActionResult Index()
        {
            var productView = new ProductListViewModel()
            {
                ProductList = _productService.GetHomePageProducts(),
                Account = SQLUserRepository.acc
            };
            return View(productView);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
