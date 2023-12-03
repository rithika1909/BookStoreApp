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
    public class OrderSummaryBusiness : IOrderSummaryBusiness
    {
        public readonly IOrderSummaryRepo summaryRepo;
        NlogUtility nlog = new NlogUtility();

        public OrderSummaryBusiness(IOrderSummaryRepo summaryRepo)
        {
            this.summaryRepo = summaryRepo;
        }

        public IEnumerable<OrderSummary> GetOrderSummary(int OrderId, int UserId)
        {
            try
            {
                var result = this.summaryRepo.GetOrderSummary(OrderId, UserId);
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
