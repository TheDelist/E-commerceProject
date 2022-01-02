using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.webui.Models
{
    public class ShoppingCartListViewModel
    {
        public List<Product> Products { get; set; }

        public List<Order> Orders { get; set; }
        public Account User { get; set; }
    }
}