using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.webui.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategories { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
        }
    }
    public class ProductListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> ProductList { get; set; }
    }
}