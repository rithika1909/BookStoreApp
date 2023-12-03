using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Utility;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPlacedController : ControllerBase
    {
        public readonly IOrderPlacedBusiness orderBusiness;

        NlogUtility nlog = new NlogUtility();
        public OrderPlacedController(IOrderPlacedBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(int CartId,int CustomerId)
        {
            try
            {
                var result =  this.orderBusiness.PlaceOrder(CartId,CustomerId);
                if (result != null)
                {
                    nlog.LogInfo("Placed Order Successfully");
                    return this.Ok(new { Status = true, Message = "Placed Order Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Placed Order  Unsuccessful"});
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }

    }
}
