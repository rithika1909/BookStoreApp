using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface ICustomerFeedbackBusiness
    {
        public Task<int> AddFeedback(CustomerFeedback obj,int UserId);

        public IEnumerable<CustomerFeedback> GetAllFeedback(int UserId);
    }
}
