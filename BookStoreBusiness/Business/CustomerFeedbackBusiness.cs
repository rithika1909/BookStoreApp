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
    public class CustomerFeedbackBusiness : ICustomerFeedbackBusiness
    {
        public readonly ICustomerFeedbackRepo feedbackRepo;

        NlogUtility nlog= new NlogUtility();
        public CustomerFeedbackBusiness(ICustomerFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }
        public Task<int> AddFeedback(CustomerFeedback obj, int UserId)
        {
            try
            {
                var result = this.feedbackRepo.AddFeedback(obj, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<CustomerFeedback> GetAllFeedback(int UserId)
        {
            try
            {
                var result = this.feedbackRepo.GetAllFeedback(UserId);
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
