using BookStoreCommon.UserRegister;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IUserRepo
    {
        public Task<int> UserRegistration(UserRegister obj);

        public string UserLogin(string email, string password);


    }
}
