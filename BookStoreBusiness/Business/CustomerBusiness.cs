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
    public class CustomerBusiness : ICustomerBusiness
    {
        public readonly ICustomerRepo customerRepo;

        NlogUtility nlog = new NlogUtility();
        public CustomerBusiness(ICustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }
        public Task<int> AddAddress(CustomerDetails obj,int UserId)
        {
            try
            {
                var result = this.customerRepo.AddAddress(obj, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public bool UpdateAddress(CustomerDetails obj,int UserId)
        {
            try
            {
                var result = this.customerRepo.UpdateAddress(obj, UserId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public bool DeleteAddress(int UserId, int CustomerId)
        {
            try
            {
                var result = this.customerRepo.DeleteAddress(UserId, CustomerId);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<CustomerDetails> GetAddressById(int UserId)
        {
            try
            {
                var result = this.customerRepo.GetAddressById(UserId);
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
