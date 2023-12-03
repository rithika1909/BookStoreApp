using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Utility;

namespace BookStoreRepository.Repository 
{
    public class OrderSummaryRepo : IOrderSummaryRepo
    {
        private readonly IConfiguration iconfiguration;

        NlogUtility nlog = new NlogUtility();
        public OrderSummaryRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }

        public IEnumerable<OrderSummary> GetOrderSummary(int OrderId,int UserId)
        {
            try
            {
                Connection();
                List<OrderSummary> SummaryList = new List<OrderSummary>();
                SqlCommand com = new SqlCommand("spGetSummary", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@OrderId", OrderId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {

                    SummaryList.Add(
                        new OrderSummary()
                        {

                            summaryId = Convert.ToInt32(dr["summaryId"]),
                            OrderId = Convert.ToInt32(dr["OrderId"]),
                            OrderPlaced = new OrderPlaced()
                            {
                                OrderId = Convert.ToInt32(dr["OrderId"]),
                                CustomerId = Convert.ToInt32(dr["CustomerId"]),
                                // CustomerDetails = new CustomerDetails(),
                                CartId = Convert.ToInt32(dr["CartId"]),
                                Cart = new Cart()
                                {
                                    UserId= Convert.ToInt32(dr["UserId"]),
                                    BookCount = Convert.ToInt32(dr["BookCount"]),
                                    Book = new Book()
                                    {
                                        BookId = Convert.ToInt32(dr["BookId"]),
                                        BookName = dr["BookName"].ToString(),
                                        BookDescription = dr["BookDescription"].ToString(),
                                        BookAuthor = dr["BookAuthor"].ToString(),
                                        BookImage = Convert.ToString(dr["BookImage"]),
                                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                        Rating = Convert.ToInt32(dr["Rating"])

                                    }

                                }
                            }
                        });
                    //CustomerDetails CustomerDetails = new CustomerDetails()
                    //{
                    //    CustomerId = Convert.ToInt32(dr["CustomerId"]),
                    //    TypeId = Convert.ToInt32(dr["TypeId"]),
                    //    FullName = Convert.ToString(dr["FullName"]),
                    //    Address = Convert.ToString(dr["Address"]),
                    //    MobileNumber = Convert.ToString(dr["MobileNumber"]),
                    //    CityOrTown = Convert.ToString(dr["CityOrTown"]),
                    //    State = Convert.ToString(dr["State"])
                    //};


                    // Include other book details based on the columns retrieved


                }
                nlog.LogDebug("Retrieved Order Summary");
                return SummaryList;

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
