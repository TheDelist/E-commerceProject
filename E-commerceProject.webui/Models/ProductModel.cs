using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_commerceProject.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public bool IsHome { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}