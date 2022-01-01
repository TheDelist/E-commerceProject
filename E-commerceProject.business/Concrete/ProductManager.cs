using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository=productRepository;
            
        }

        public int Count()
        {
           return _productRepository.Count();
        }

        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public void Delete(int id)
        {
           _productRepository.Delete(id);
        }

        public List<Product> GetAll(int? page=1,int? pageSize=3)
        {
            return _productRepository.GetAll((int)page, (int)pageSize);
        }
          public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
           return _productRepository.GetHomePageProducts();
        }

        public List<Product> GetProductByCategory(string productname,int page,int pageSize) 
        {
            return _productRepository.GetProductsByCategory(productname, page, pageSize);
        }

        public Product GetProductDetails(string productname)
        {
           return  _productRepository.GetProductDetails(productname);
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _productRepository.GetSearchResult(searchString);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public string ErrorMessage { get; set; }

        public bool Validation(Product entity) // Extra business rules can be added here
        {
            var isValid = true;
            // if(string.IsNullOrEmpty(entity.Name)) 
            // {
            //     ErrorMessage += "The Name field is required!\n";
            //     isValid=false;
            // }
            return isValid;
        }
    }
}