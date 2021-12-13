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
        public int Create(Product entity)
        {
            return  _productRepository.Create(entity);
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

        public List<Product> GetProductByCategory(string productname,int page,int pageSize) 
        {
            return _productRepository.GetProductsByCategory(productname, page, pageSize);
        }

        public List<Product> GetProductByCategory(string name)
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(string productname)
        {
           return  _productRepository.GetProductDetails(productname);
        }

        

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }
    }
}