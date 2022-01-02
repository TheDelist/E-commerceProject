using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.entity;

namespace E_commerceProject.data.Abstract
{
    public interface IUserRepository
    {
        void Register(Account Account);
        void Delete(int id); int Count(int UserId);
        Account GetById(int id);
        Account Login2(String Username, String Password);
    }
}