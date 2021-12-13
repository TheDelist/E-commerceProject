using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceProject.entity
{
    public class Product
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
        public Category Category { get; set; }
    }
}