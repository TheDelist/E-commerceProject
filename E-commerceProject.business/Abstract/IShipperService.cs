using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.business.Abstract
{
    public interface IShipperService
    {
        List<Shipper> GetAll();
    }
}