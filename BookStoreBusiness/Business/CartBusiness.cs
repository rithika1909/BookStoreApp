using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class CartBusiness : ICartBusiness
    {
        public readonly ICartRepo cartRepo;

        NlogUtility nlog = new NlogUtility();
        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }

        public Task<int> AddToCart(Cart cart, int UserId)
        {
            try
            {
                var result = this.cartRepo.AddToCart(cart, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }
        public IEnumerable<Cart> GetAllCart(int UserId)
        {
            try
            {
                var result = this.cartRepo.GetAllCart(UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public bool UpdateCartList(Cart cartlist, int UserId)
        {
            try
            {
                var result = this.cartRepo.UpdateCartList(cartlist, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public bool DeleteCart(int BookId)
        {
            try
            {
                var result = this.cartRepo.DeleteCart(BookId);
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
