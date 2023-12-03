using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IWishListRepo
    {
        public Task<int> AddBookToWishList(WishList obj,int UserId);

        public IEnumerable<WishList> GetAllWishListBooks(int UserId);

        public bool UpdateWishList(WishList wishlist,int UserId);

        public bool DeleteWishList(int WishListId);




    }
}
