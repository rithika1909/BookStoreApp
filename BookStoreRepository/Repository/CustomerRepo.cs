using BookStoreCommon.Book;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookStoreRepository.IRepository;
using BookStoreCommon.User;
using Utility;

namespace BookStoreRepository.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IConfiguration iconfiguration;

        NlogUtility nlog = new NlogUtility();
        public CustomerRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionStr);
        }
        public async Task<int> AddAddress(CustomerDetails obj,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spAddDetails ", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FullName", obj.FullName);
                com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@CityOrTown", obj.CityOrTown);
                com.Parameters.AddWithValue("@State", obj.State);
                com.Parameters.AddWithValue("@TypeId", obj.TypeId);
                com.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                nlog.LogDebug("Details Added");
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

        public bool UpdateAddress(CustomerDetails obj,int UserId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spUpdateAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                com.Parameters.AddWithValue("@FullName", obj.FullName);
                com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@CityOrTown", obj.CityOrTown);
                com.Parameters.AddWithValue("@State", obj.State);
                com.Parameters.AddWithValue("@TypeId", obj.TypeId);
                com.Parameters.AddWithValue("@UserId", UserId);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Updated Address");
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

        public bool DeleteAddress(int UserId,int CustomerId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("spDeleteAddress", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                com.Parameters.AddWithValue("@CustomerId", CustomerId);

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    nlog.LogDebug("Deleted Details");
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

        public IEnumerable<CustomerDetails>GetAddressById(int UserId)
        {
            try
            {
                Connection();
                List<CustomerDetails> customerDetails = new List<CustomerDetails>();
                SqlCommand com = new SqlCommand("spGetAddressById", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    customerDetails.Add(
                        new CustomerDetails()
                        {
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            UserId = Convert.ToInt32(dr["UserId"]),
                            TypeId = Convert.ToInt32(dr["TypeId"]),
                            FullName = Convert.ToString(dr["FullName"]),
                            MobileNumber = Convert.ToString(dr["MobileNumber"]),
                            Address = Convert.ToString(dr["Address"]),
                            CityOrTown = Convert.ToString(dr["CityOrTown"]),
                            State = Convert.ToString(dr["State"]),
                            Types = new Types()
                            {
                                TypeId = Convert.ToInt32(dr["TypeId"]),
                                TypeName = dr["TypeName"].ToString(),
                            },
                        }
                        );
                }
                nlog.LogDebug("Retreived Address by Id");
                return customerDetails;
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

            //Connection();
            //CustomerDetails customerDetails = null;

            //SqlCommand com = new SqlCommand("spGetAddressById", con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.AddWithValue("@UserId", UserId);

            //SqlDataAdapter da = new SqlDataAdapter(com);
            //DataTable dt = new DataTable();
            //con.Open();
            //da.Fill(dt);
            //con.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt.Rows[0];
            //    customerDetails = new CustomerDetails()
            //    {
            //        UserId= Convert.ToInt32(dr["UserId"]),
            //        TypeId = Convert.ToInt32(dr["TypeId"]),
            //        FullName = Convert.ToString(dr["FullName"]),
            //        Address = Convert.ToString(dr["Address"]),
            //        MobileNumber = Convert.ToString(dr["MobileNumber"]),
            //        CityOrTown = Convert.ToString(dr["CityOrTown"]),
            //        State = Convert.ToString(dr["State"]),
            //        Types = new Types()
            //        {
            //            TypeId = Convert.ToInt32(dr["TypeId"]),
            //            TypeName = dr["TypeName"].ToString(),
            //        },

            //    };
            //}

          
        //}
    }
}
