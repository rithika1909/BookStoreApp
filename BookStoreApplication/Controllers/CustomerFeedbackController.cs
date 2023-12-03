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
    public class CustomerFeedbackController : ControllerBase
    {
        public readonly ICustomerFeedbackBusiness feedbackBusiness;

        public int userId;

        NlogUtility nlog = new NlogUtility();
        public CustomerFeedbackController(ICustomerFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }

        [HttpPost]
        [Route("AddFeedback")]
        public ActionResult  AddFeedback(CustomerFeedback obj)
        {

            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.feedbackBusiness.AddFeedback(obj, userId);
                if (result != null)
                {
                    nlog.LogInfo("Feedback Added Successfully");
                    return this.Ok(new { Status = true, Message = "Feedback Added Successfully" , Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Feedback Added Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllFeedback")]
        public ActionResult GetAllFeedback()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.feedbackBusiness.GetAllFeedback(userid);
                if (result != null)
                {
                    nlog.LogInfo("Got Feedback Successfully");
                    return this.Ok(new { Status = true, Message = "All Feedbacks Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Feedbacks not Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

    }
}
