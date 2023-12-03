using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface ICartRepo
    {
        public Task<int> AddToCart(Cart cart, int UserId);
        public IEnumerable<Cart> GetAllCart(int UserId);
        public bool UpdateCartList(Cart cartlist, int UserId);

        public bool DeleteCart(int BookId);




    }
}
