using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.business.Abstract
{
    public interface IProductService
    {
        List<Product> GetProductByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string productname);
        Product GetById(int id);

        List<Product> GetAll(int? page,int? pageSize);
        List<Product> GetAll();

        int Create(Product entity);

        void Update(Product entity);
        void Delete(int id);
    }
}