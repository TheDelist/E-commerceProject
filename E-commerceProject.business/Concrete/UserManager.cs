using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.business.Concrete
{
    public class UserManager : IUserService
    {

        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public int Count(int UserId)
        {
            return _userRepository.Count(UserId);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public Account GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public Account Login(string UserName, string Password)
        {
            return _userRepository.Login2(UserName, Password);
        }

        public void Register(Account Account)
        {
            _userRepository.Register(Account);
        }
    }
}