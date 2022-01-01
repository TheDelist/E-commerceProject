using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_commerceProject.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage="The Name field is required!")]
        [StringLength(60,MinimumLength=3,ErrorMessage="The field Name must be between 5-60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage="The Price field is required!")]
        [Range(1,100000,ErrorMessage="The field Price must be between 1-1000000.")]
        public double? Price { get; set; }

        [Required(ErrorMessage="The Description field is required!")]
        [StringLength(100,MinimumLength=5,ErrorMessage="The field Description must be between 5-100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage="The Url field is required!")]
        public string Url { get; set; }

        [Required(ErrorMessage="The CategoryId field is required!")]
        public int CategoryId { get; set; }

        [Range(0,100000,ErrorMessage="The field Quantity must be between 1-1000000.")]
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public bool IsHome { get; set; }
        public List<SelectListItem> allcategories { get; set; }
    }
}