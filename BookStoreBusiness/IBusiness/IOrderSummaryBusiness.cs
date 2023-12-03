using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusiness.IBusiness
{
    public interface IOrderSummaryBusiness
    {
        public IEnumerable<OrderSummary> GetOrderSummary(int OrderId,int UserId);

    }
}
