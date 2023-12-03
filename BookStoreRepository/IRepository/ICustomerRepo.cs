using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface ICustomerRepo
    {
        public Task<int> AddAddress(CustomerDetails obj, int UserId);

        public bool UpdateAddress(CustomerDetails obj, int UserId);

        public bool DeleteAddress(int UserId, int CustomerId);

        public IEnumerable<CustomerDetails> GetAddressById(int UserId);



    }
}
