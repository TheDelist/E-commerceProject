using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.business.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        List<Product> GetProductByCategory(string name,int page,int pageSize);
        Product GetProductDetails(string productname);
        Product GetById(int id);
        List<Product> GetAll(int? page,int? pageSize);
        List<Product> GetAll();
        List<Product> GetHomePageProducts();
        List<Product> GetSearchResult(string searchString);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(int id);
        int GetCountByCategory(string category);
        int Count();
    }
}