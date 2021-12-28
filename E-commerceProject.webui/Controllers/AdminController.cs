using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.entity;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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

        public IActionResult ProductList()
        {
            return View(new ProductListViewModel
            {
                ProductList = _productService.GetAll()
            });
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
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
            };
            _productService.Create(newProduct);
            // ViewData veya ViewBag i sadece View e yönlendirme yaaprken kullanabiliriz.
            // Farklı bir action metoduna yönlendirme yapıyorsak TempData kullanmamız gerekir.
            var msg = new AlertMessage()
            {
                Message = $"{newProduct.Name} was created!",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            // Json objesi {"Message":"x was created.","AlertType":"success"} şeklinde gidiyor. Cannot serialize hatasından kurtuluyoruz.
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            var entity = new Category(){
                Name = model.Name,
                Url = model.Url
            };
            _categoryService.Create(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} was created!",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if(entity==null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,   
                Name = entity.Name,
                Url = entity.Url,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                IsHome = entity.IsHome,
                InStock = entity.InStock,
                Quantity = entity.Quantity
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ProductEdit(ProductModel model)
        {
            var entity = _productService.GetById(model.ProductId);
            if(entity==null)
            {
                return NotFound();
            }
            if(model.Quantity == 0)
            {
                model.InStock = false;
            } else
            {
                model.InStock = true;
            }
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Description = model.Description;
            entity.Url = model.Url;
            entity.Quantity = model.Quantity;
            entity.ImageUrl = model.ImageUrl;
            entity.CategoryId = model.CategoryId;
            entity.InStock = model.InStock;
            entity.IsHome = model.IsHome;
            
            _productService.Update(entity);
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} was updated!",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetByIdWithProducts((int)id);
            if(entity==null)
            {
                return NotFound();
            }
            var model = new CategoryModel(){
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.Products
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.CategoryId);
            if(entity==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Url = model.Url;

            _categoryService.Update(entity);
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} was updated!",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList"); 
        }

        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if(entity!=null)
            {
                _productService.Delete(productId);
            }
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} was deleted!",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if(entity!=null)
            {
                _categoryService.Delete(categoryId);
            }
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} was deleted!",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
    }
}