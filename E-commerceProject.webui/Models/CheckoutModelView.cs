using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.webui.Models
{
    public class CheckoutModelView
    {
        public int TotalSum { get; set; }
        public Account User { get; set; }
        public List<Shipper> Shippers { get; set; }

    }
}