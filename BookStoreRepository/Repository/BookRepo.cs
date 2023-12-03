using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreRepository.Repository
{
    public class BookRepo :IBookRepo
    {
        private readonly IConfiguration iconfiguration;

        NlogUtility nlog = new NlogUtility();
        public BookRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddBook(Book obj)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddBook", con);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.AddWithValue("@BookName", obj.BookName);
                com.Parameters.AddWithValue("@BookDescription", obj.BookDescription);
                com.Parameters.AddWithValue("@BookAuthor", obj.BookAuthor);
                com.Parameters.AddWithValue("@BookImage", obj.BookImage);
                com.Parameters.AddWithValue("@BookCount", obj.BookCount);
                com.Parameters.AddWithValue("@BookPrice", obj.BookPrice);
                com.Parameters.AddWithValue("@Rating", obj.Rating);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Book Added Successfuly");
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

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                Connection();
                List<Book> BookList = new List<Book>();
                SqlCommand com = new SqlCommand("spGetAllBooks", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    BookList.Add(
                        new Book()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            BookName = Convert.ToString(dr["BookName"]),
                            BookDescription = Convert.ToString(dr["BookDescription"]),
                            BookAuthor = Convert.ToString(dr["BookAuthor"]),
                            BookImage = Convert.ToString(dr["BookImage"]),
                            BookCount = Convert.ToInt32(dr["BookCount"]),
                            BookPrice = Convert.ToInt32(dr["BookPrice"]),
                            Rating = Convert.ToInt32(dr["Rating"]),

                        }
                        );
                }
                foreach (var data in BookList)
                {
                    Console.WriteLine(data.BookId + "" + data.BookName);
                }
                nlog.LogDebug("Retrieved Books");
                return BookList;

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

        public bool UpdateBook(Book obj)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUpdateBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", obj.BookId);
                com.Parameters.AddWithValue("@BookName", obj.BookName);
                com.Parameters.AddWithValue("@BookDescription", obj.BookDescription);
                com.Parameters.AddWithValue("@BookAuthor", obj.BookAuthor);
                com.Parameters.AddWithValue("@BookImage", obj.BookImage);
                com.Parameters.AddWithValue("@BookCount", obj.BookCount);
                com.Parameters.AddWithValue("@BookPrice", obj.BookPrice);
                com.Parameters.AddWithValue("@Rating", obj.Rating);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Book Updated Successfuly");
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

        public bool DeleteBook(int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Deleted Book");
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

        public bool Image(IFormFile file, int BookId)
        {
            try
            {
                if (file == null)
                {
                    return false;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account(
                     "dmhy5nfe9",
                     "953275358872337",
                     "oivi7czM8MuthnTkn5GNxW_MHdY");

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                var cloudinaryfilelink = uploadResult.Uri.ToString();
                Link(cloudinaryfilelink, BookId);
                nlog.LogDebug("Image Uploaded");
                return true;
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void Link(string cloudinaryfilelink, int BookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUploadImage", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", BookId);
                com.Parameters.AddWithValue("@fileLink", cloudinaryfilelink);
                con.Open();
                var i = com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                nlog.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Book GetBookById(int bookId)
        {
            try
            {
                Connection();
                Book book = null;

                SqlCommand com = new SqlCommand("spGetBookById", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", bookId);

                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    book = new Book()
                    {

                        BookId = Convert.ToInt32(dr["BookId"]),
                        BookName = Convert.ToString(dr["BookName"]),
                        BookDescription = Convert.ToString(dr["BookDescription"]),
                        BookAuthor = Convert.ToString(dr["BookAuthor"]),
                        BookImage = Convert.ToString(dr["BookImage"]),
                        BookCount = Convert.ToInt32(dr["BookCount"]),
                        BookPrice = Convert.ToInt32(dr["BookPrice"]),
                        Rating = Convert.ToInt32(dr["Rating"])
                    };
                }
                nlog.LogDebug("Got BookById");
                return book;

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
