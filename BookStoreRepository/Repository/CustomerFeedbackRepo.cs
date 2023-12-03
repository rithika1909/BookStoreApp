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
    public class CustomerFeedbackRepo : ICustomerFeedbackRepo
    {
        private readonly IConfiguration iconfiguration;

        NlogUtility nlog= new NlogUtility();
        public CustomerFeedbackRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddFeedback(CustomerFeedback obj,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddFeedback ", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@BookId", obj.BookId);
                com.Parameters.AddWithValue("@CustomerDescription", obj.CustomerDescription);
                com.Parameters.AddWithValue("@Ratings", obj.Ratings);
                
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Feedback Added Successfuly");
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
        public IEnumerable<CustomerFeedback> GetAllFeedback(int UserId)
        {
            try
            {
                Connection();
                List<CustomerFeedback> feedback = new List<CustomerFeedback>();
                SqlCommand com = new SqlCommand("spGetAllFeedback", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    feedback.Add(
                       new CustomerFeedback()
                       {
                           FeedbackId = Convert.ToInt32(dr["FeedbackId"]),
                           UserId = Convert.ToInt32(dr["UserId"]),
                           BookId = Convert.ToInt32(dr["BookId"]),
                           CustomerDescription = Convert.ToString(dr["CustomerDescription"]),
                           Ratings = Convert.ToInt32(dr["Ratings"])
                       }
                       );
                }
                nlog.LogDebug("Got all Books");
                return feedback;
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
