using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.webui.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage="The Name field is required!")]
        [StringLength(60,MinimumLength=3,ErrorMessage="The field Name must be between 5-60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage="The Url field is required!")]
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}