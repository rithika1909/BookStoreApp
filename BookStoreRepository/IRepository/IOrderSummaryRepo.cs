using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IOrderSummaryRepo
    {
        public IEnumerable<OrderSummary> GetOrderSummary(int OrderId ,int UserId);

    }
}
