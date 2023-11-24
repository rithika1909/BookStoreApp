using BookStoreCommon.UserRegister;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface IUserBusiness
    {
        public Task<int> UserRegistration(UserRegister obj);

        public string UserLogin(string email, string password);
    }
}
