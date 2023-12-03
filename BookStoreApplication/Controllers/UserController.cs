using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BookStoreBusiness.IBusiness;

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookStoreCommon.User;
using NLog.Fluent;
using Utility;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBusiness userBusiness;
        NlogUtility nlog = new NlogUtility();
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("UserRegistration")]
        public ActionResult UserRegistration(UserRegister userRegister)
        {
            try
            {
                var result = this.userBusiness.UserRegistration(userRegister);
                if (result != null)
                {
                    nlog.LogInfo("Registered Successfully");
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
                    nlog.LogInfo("User Logged In Successfully");
                    return this.Ok(new { Status = true, Message = "User Logged In Successfully", Data = result, id = Id });
                }
                return this.BadRequest(new { Status = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]

        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = this.userBusiness.ForgetPassword(email);

                if (resultLog != null)
                {
                    nlog.LogInfo("Forgot mail sent Successfully");
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult UserResetPassword(string newpassword,string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userBusiness.ResetPassword(email, newpassword,confirmpassword);
                if (result != null)
                {
                    nlog.LogInfo("User Password Reset Successfully");
                    return this.Ok(new { Status = true, Message = "User Password Reset Successful", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Password Reset Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}