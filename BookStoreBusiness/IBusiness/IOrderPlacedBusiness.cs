﻿using BookStoreCommon.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface IOrderPlacedBusiness
    {
        public Task<int> PlaceOrder(int CartId, int CustomerId);

    }
}
