using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Utility;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController :ControllerBase
    {

        public readonly ICustomerBusiness customerBusiness;

        public int userId;

        NlogUtility nlog = new NlogUtility();
        public CustomerController(ICustomerBusiness customerBusiness)
        {
            this.customerBusiness = customerBusiness;
        }

        [HttpPost]
        [Route("AddAddress")]
        public ActionResult AddAddress(CustomerDetails obj)
        {

            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerBusiness.AddAddress(obj, userId);
                if (result != null)
                {
                    nlog.LogInfo("CustomerDetails Added Successfully");
                    return this.Ok(new { Status = true, Message = "Details Added Successfully", Data = obj});
                }
                return this.BadRequest(new { Status = false, Message = "Adding Details Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateAddress")]
        public ActionResult UpdateAddress(CustomerDetails obj)
        {

            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerBusiness.UpdateAddress(obj, userId);
                if (result)
                {
                    nlog.LogInfo("Updated Address Successfully");
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("DeleteAddress")]
        public ActionResult DeleteAddress(int CustomerId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerBusiness.DeleteAddress(CustomerId,userId);
                if (result)
                {
                    nlog.LogInfo("Addredd deleted Successfully");
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully"});
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAddressById")]
        public ActionResult GetAddressById()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerBusiness.GetAddressById(userId);
                if (result != null)
                {
                    nlog.LogInfo("Retrieved Successfully");
                    return this.Ok(new { Status = true, Message = " Details Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Details Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
