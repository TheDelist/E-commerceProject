using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.entity
{
    public class Shipper
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public int Price { get; set; }
        public int ShipperID { get; set; }
    }
}