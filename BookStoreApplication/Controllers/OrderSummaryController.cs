using BookStoreBusiness.IBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Utility;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSummaryController : ControllerBase
    {
        public readonly IOrderSummaryBusiness summaryBusiness;

        public int userId;

        NlogUtility nlog = new NlogUtility();
        public OrderSummaryController(IOrderSummaryBusiness summaryBusiness)
        {
            this.summaryBusiness = summaryBusiness;
        }

        [HttpGet]
        [Route("GetOrderSummary")]
        public ActionResult GetOrderSummary(int OrderId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.summaryBusiness.GetOrderSummary(OrderId, userId);
                if (result != null)
                {
                    nlog.LogInfo("Got Summary Successfully");
                    return this.Ok(new { Status = true, Message = "Placed Order Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }


    }
}
