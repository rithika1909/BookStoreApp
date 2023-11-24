using BookStoreCommon.UserRegister;
using BookStoreRepository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration iconfiguration;
        public UserRepo(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        private SqlConnection con;
        private void Connection()
        {
            string connectionStr = this.iconfiguration[("ConnectionStrings:UserDbConnection")];

            con = new SqlConnection(connectionStr);
        }
        public async Task<int> UserRegistration(UserRegister obj)
        {
            var password=EncryptPassword(obj.Password);
            obj.Password = password;    
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("UserRegistration", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FullName", obj.FullName);
                com.Parameters.AddWithValue("@EmailId", obj.EmailId);
                com.Parameters.AddWithValue("@Password", obj.Password);
                com.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
                con.Open();
                int result = await com.ExecuteNonQueryAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public string UserLogin(string email, string password)
        {
            UserRegister userregister = GetUser(email);
            try
            {
                var decryptPassword = DecryptPassword(userregister.Password);
                if (userregister != null && decryptPassword.Equals(password))
                {
                    var token = GenerateSecurityToken(userregister.EmailId, userregister.UserId);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public UserRegister GetUser(string email)
        {
            var obj = new UserRegister();
            Connection();
            con.Open();
            SqlCommand com = new SqlCommand("GetUser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmailId", email);
            //SqlDataAdapter da = new SqlDataAdapter(com);
            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                obj = new UserRegister
                {
                    UserId = (int)reader["UserId"],
                    FullName = (string)reader["FullName"],
                    EmailId = (string)reader["EmailId"],
                    Password = (string)reader["Password"],
                    MobileNumber = (string)reader["MobileNumber"]
                };
            }
            con.Close();
            return obj;

        }
       
        public string GenerateSecurityToken(string email, int userId)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
        public string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string DecryptPassword(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new string(decoded_char);
            return decryptpwd;
        }
    }
}