using BookStoreCommon.Book;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookStoreRepository.IRepository;
using Utility;

namespace BookStoreRepository.Repository
{
    public class PlaceOrderRepo : IOrderPlacedRepo
    {
        private readonly IConfiguration iconfiguration;
        NlogUtility nlog = new NlogUtility();
        public PlaceOrderRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> PlaceOrder(int CartId,int CustomerId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spPlaceOrder", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CartId", CartId);
                com.Parameters.AddWithValue("@CustomerId",CustomerId);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Order Placed");

                return result;
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
