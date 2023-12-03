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
    public class OrderPlacedBusiness : IOrderPlacedBusiness
    {
        public readonly IOrderPlacedRepo orderRepo;

        NlogUtility nlog = new NlogUtility();
        public OrderPlacedBusiness(IOrderPlacedRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        public  Task<int> PlaceOrder(int CartId, int CustomerId)
        {
            try
            {
                var result = this.orderRepo.PlaceOrder(CartId, CustomerId);
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
