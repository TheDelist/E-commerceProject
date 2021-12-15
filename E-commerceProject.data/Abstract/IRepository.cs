using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceProject.data.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);


        List<T> GetAll();

        int Create(T entity);

        void Update(T entity);
        void Delete(int id);
    }
}