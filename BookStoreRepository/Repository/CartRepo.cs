using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreRepository.Repository
{

    public class CartRepo: ICartRepo
    {
        private readonly IConfiguration iconfiguration;

        NlogUtility nlog= new NlogUtility();
        public CartRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }

        public async Task<int> AddToCart(Cart cart,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddToCart", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@BookId", cart.BookId);
                com.Parameters.AddWithValue("@BookCount", cart.BookCount);

                con.Open();

                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Added to Cart");

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

        public IEnumerable<Cart> GetAllCart(int UserId)
        {
            try
            {
                Connection();
                List<Cart> CartList = new List<Cart>();
                SqlCommand com = new SqlCommand("spGetAllCart", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    CartList.Add(
                        new Cart()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            UserId = Convert.ToInt32(dr["UserId"]),
                            CartId = Convert.ToInt32(dr["CartId"]),
                            BookCount = Convert.ToInt32(dr["BookCount"]),

                            Book = new Book() // Create a new Book object and populate its properties
                            {
                                BookName = dr["BookName"].ToString(),
                                BookDescription = dr["BookDescription"].ToString(),
                                BookAuthor = dr["BookAuthor"].ToString(),
                                BookImage = Convert.ToString(dr["BookImage"]),
                                BookPrice = Convert.ToInt32(dr["BookPrice"]),
                                Rating = Convert.ToInt32(dr["Rating"])
                                // Include other book details based on the columns retrieved
                            },
                            IsAvailable = Convert.ToBoolean(dr["IsAvailable"])

                        }
                    );
                }
                nlog.LogDebug("RetrievedCart");
                return CartList;

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

        public bool UpdateCartList(Cart cartlist,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUpdateCart", con);
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@WishListId", cartlist.WishListId);
                //com.Parameters.AddWithValue("@CartId", cartlist.CartId);
                com.Parameters.AddWithValue("@UserId", UserId);



                com.Parameters.AddWithValue("@BookId", cartlist.BookId);
                com.Parameters.AddWithValue("@BookCount", cartlist.BookCount);


                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Updated Cart");
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

        public bool DeleteCart(int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteCart", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", BookId);


                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Deleted from Cart");
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
