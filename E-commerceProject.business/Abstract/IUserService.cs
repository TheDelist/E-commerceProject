using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.business.Abstract
{
    public interface IUserService
    {
        Account GetById(int id);
        void Register(Account Account);
        void Delete(int id);
        int Count(int UserId);
        Account Login(String UserName, String Password);

    }
}