using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        Product GetProductDetails(string productname);
        List<Product> GetPopularProducts();
        List<Product> GetSearchResult(string searchString);
        List<Product> GetAll(int? page, int? pageSize);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        int Count();
    }
}