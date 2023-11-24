using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BookStoreBusiness.IBusiness;

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using BookStoreCommon.UserRegister;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("UserRegistration")]
        public async Task<ActionResult> UserRegistration(UserRegister userRegister)
        {
            try
            {
                var result = await this.userBusiness.UserRegistration(userRegister);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "User Registered Successfully", Data = userRegister });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UserLogin")]
        public ActionResult UserLogin(string email, string password)
        {
            try
            {
                var result = this.userBusiness.UserLogin(email, password);
                if (result != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenhandler.ReadJwtToken(result);
                    var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");
                    string Id = id.Value;
                    return this.Ok(new { Status = true, Message = "User Logged In Successfully", Data = result, id = Id });
                }
                return this.BadRequest(new { Status = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
    }
}