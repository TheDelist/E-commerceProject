using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.entity;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_commerceProject.webui.Controllers
{
    public class AdminController:Controller
    {
          private IProductService _productService;
          private ICategoryService _categoryService;

          public AdminController(IProductService productService,ICategoryService categoryService)
          {
              _productService=productService;
              _categoryService=categoryService;
          }

          [HttpGet]
        public IActionResult ProductCreate()
        {
            // var categoryList = _categoryService.GetAll();

            // List<SelectListItem> item = categoryList.ConvertAll(Category =>
            //     {
            //         return new SelectListItem()
            //         {
            //             Text = Category.Name,
            //             Value = Category.CategoryId.ToString()
            //         };
            //     });

            // item.Insert(0, new SelectListItem()
            // {
            //     Text = "Select Category",
            //     Value = String.Empty
            // });
            // ViewBag.ListOfCategories = item;
            return View();
        }
        [HttpPost]
        public IActionResult ProductCreate(ProductModel product)
        {

            var newProduct = new Product()
            {
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Url = product.Url,
                Description = product.Description,
                CategoryId = product.CategoryId,
                InStock = product.InStock,

            };
            _productService.Create(newProduct);
            return RedirectToAction("ProductList");
        }
    }
}