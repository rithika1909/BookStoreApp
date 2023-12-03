﻿using BookStoreCommon.User;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IUserRepo
    {
        public Task<int> UserRegistration(UserRegister obj);

        public string UserLogin(string email, string password);

        public string ForgetPassword(string email);

        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword);



    }
}
