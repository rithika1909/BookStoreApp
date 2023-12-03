using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using BookStoreRepository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class WishListBusiness : IWishListBusiness
    {
        public readonly IWishListRepo wishlistRepo;

        NlogUtility nlog = new NlogUtility();
        public WishListBusiness(IWishListRepo wishlistRepo)
        {
            this.wishlistRepo = wishlistRepo;
        }
        public Task<int> AddBookToWishList(WishList obj,int UserId)
        {
            try
            {
                var result = this.wishlistRepo.AddBookToWishList(obj, UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<WishList> GetAllWishListBooks(int UserId)
        {
            try
            {
                var result = this.wishlistRepo.GetAllWishListBooks(UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public bool UpdateWishList(WishList wishlist,int UserId)
        {
            try
            {
                var result = this.wishlistRepo.UpdateWishList(wishlist, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public bool DeleteWishList(int WishListId)
        {
            try
            {
                var result = this.wishlistRepo.DeleteWishList(WishListId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }




    }
}
