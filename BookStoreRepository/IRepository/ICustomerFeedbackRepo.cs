using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface ICustomerFeedbackRepo
    {
        public Task<int> AddFeedback(CustomerFeedback obj,int UserId);

        public IEnumerable<CustomerFeedback> GetAllFeedback(int UserId);


    }
}
