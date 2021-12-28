using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceProject.webui.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent  // Used in home/index view
    {
        // Dependency Injection
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            if(RouteData.Values["category"]!= null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_categoryService.GetAll());
        }
    }
}