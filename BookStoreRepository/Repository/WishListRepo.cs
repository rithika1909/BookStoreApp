using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreRepository.Repository
{
    public class WishListRepo : IWishListRepo
    {
        private readonly IConfiguration iconfiguration;

        public WishListRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }

        NlogUtility nlog = new NlogUtility();

        public async Task<int> AddBookToWishList(WishList obj,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddToWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId",UserId);
                com.Parameters.AddWithValue("@BookId", obj.BookId);
                con.Open();

                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Wishlist added");
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

        public IEnumerable<WishList> GetAllWishListBooks(int UserId)
        {
            try
            {
                Connection();
                List<WishList> WishBookList = new List<WishList>();
                SqlCommand com = new SqlCommand("spGetAllWishList", con);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    WishBookList.Add(
                        new WishList()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),

                            WishListId = Convert.ToInt32(dr["WishListId"]),
                            UserId = Convert.ToInt32(dr["UserId"]),
                            Book = new Book()
                            {
                                BookName = dr["BookName"].ToString(),
                                BookDescription = dr["BookDescription"].ToString(),
                                BookAuthor = dr["BookAuthor"].ToString(),
                                BookImage = Convert.ToString(dr["BookImage"]),
                                BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                Rating = Convert.ToInt32(dr["Rating"])

                            },
                            IsAvailable = Convert.ToBoolean(dr["IsAvailable"])

                        }
                    );
                }
                nlog.LogDebug("Retrieved Wishlist");
                return WishBookList;

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

        public bool UpdateWishList(WishList wishlist, int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUpdateWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@WishListId", wishlist.WishListId);
                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@BookId", wishlist.BookId);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Updated wishlist");
                    return true;
                }
                else
                {
                    return false;
                }
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

        public bool DeleteWishList(int WishListId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@WishListId", WishListId);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Deleted Wishlist");
                    return true;
                }
                else
                {
                    return false;
                }
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
