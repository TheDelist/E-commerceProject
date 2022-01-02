using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Concrete.MSSQL;
using E_commerceProject.entity;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace E_commerceProject.webui.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ProductList()
        {
            if (SQLUserRepository.acc == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (SQLUserRepository.acc.Type.Equals("admin"))
            {
                return View(new ProductListViewModel
                {
                    ProductList = _productService.GetAll()
                });
            }
            else
            {
                return View("UserError");
            }

        }

        public IActionResult CategoryList()
        {
            if (SQLUserRepository.acc == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (SQLUserRepository.acc.Type.Equals("admin"))
            {

                return View(new CategoryListViewModel
                {
                    Categories = _categoryService.GetAll()
                });
            }
            else
            {
                return View("UserError");
            }
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            ProductModel product = new ProductModel();
            product.allcategories = AddCategoryList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                product.allcategories = AddCategoryList();

                var selectedItem = product.allcategories.Find(p => p.Value == product.CategoryId.ToString());
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                    var newProduct = new Product()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Url = product.Url,
                        Description = product.Description,
                        CategoryId = Convert.ToInt32(selectedItem.Value)
                    };

                    if (file != null)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                        newProduct.ImageUrl = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        _productService.Create(newProduct);
                        CreateMessage($"{newProduct.Name} was created!", "success");
                        return RedirectToAction("ProductList");
                    }
                    else
                    {
                        CreateMessage($"Please upload a image!", "danger");
                    }
                }
            }
            product.allcategories = AddCategoryList();
            return View(product);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                _categoryService.Create(entity);

                CreateMessage($"{entity.Name} was created!", "success");
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if (entity == null)
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
                Quantity = entity.Quantity,
                allcategories = AddCategoryList()
            };
            var selectedItem = model.allcategories.Find(p => p.Value == model.CategoryId.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                model.allcategories = AddCategoryList();
                var entity = _productService.GetById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }
                var selectedItem = model.allcategories.Find(p => p.Value == model.CategoryId.ToString());
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                    if (model.Quantity <= 0)
                    {
                        model.InStock = false;
                    }
                    else
                    {
                        model.InStock = true;
                    }
                    entity.Name = model.Name;
                    entity.Price = model.Price;
                    entity.Description = model.Description;
                    entity.Url = model.Url;
                    entity.Quantity = model.Quantity;
                    // entity.ImageUrl = model.ImageUrl;
                    entity.CategoryId = model.CategoryId;
                    entity.InStock = model.InStock;
                    entity.IsHome = model.IsHome;

                    if (file != null)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                        entity.ImageUrl = randomName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    _productService.Update(entity);

                    CreateMessage($"{entity.Name} was updated!", "success");
                    return RedirectToAction("ProductList");
                }
            }
            model.allcategories = AddCategoryList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetByIdWithProducts((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
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
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);

                CreateMessage($"{entity.Name} was updated!", "success");
                return RedirectToAction("CategoryList");
            }
            model.Products = _categoryService.GetByIdWithProducts((model.CategoryId)).Products;
            return View(model);
        }

        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(productId);
            }

            CreateMessage($"{entity.Name} was deleted!", "danger");
            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(categoryId);
            }
            var entity2 = _categoryService.GetById(categoryId);
            if (entity2 == null)
            {
                CreateMessage($"{entity.Name} was deleted!", "danger");
                return RedirectToAction("CategoryList");
            }
            CreateMessage($"{entity.Name} category cannot be deleted because it is a product belonging to this category!", "danger");
            return RedirectToAction("CategoryList");
        }

        // ViewData veya ViewBag i sadece View e yönlendirme yaaprken kullanabiliriz.
        // Farklı bir action metoduna yönlendirme yapıyorsak TempData kullanmamız gerekir.
        private void CreateMessage(string message, string alerttype)
        {
            var msg = new AlertMessage()
            {
                Message = message,
                AlertType = alerttype
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }

        public List<SelectListItem> AddCategoryList()
        {
            List<SelectListItem> allCategories = new List<SelectListItem>();
            var categories = _categoryService.GetAll();
            foreach (var item in categories)
            {
                allCategories.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.CategoryId.ToString()
                });
            }
            return allCategories;
        }
    }
}