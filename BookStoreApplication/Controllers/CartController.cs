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
    public class CartController : ControllerBase
    {
        public readonly ICartBusiness cartBusiness;
        public int userid;

        NlogUtility nlog= new NlogUtility();
        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }
       

        [HttpPost]
        [Route("AddCart")]
        public ActionResult AddToCart(Cart cart)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.cartBusiness.AddToCart(cart, userid);
                if (result != null)
                {
                    nlog.LogInfo("Cart Added Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Added Sucessfully", data = cart });


                }
                return this.BadRequest(new { Status = false, Message = "Adding cart Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllCart")]
        public async Task<ActionResult> GetAllCart()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.cartBusiness.GetAllCart(userid);
                if (result != null)
                {
                    nlog.LogInfo(" Cart Retrieved Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Cart Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateCart")]
        public ActionResult UpdateCartList(Cart cartList, int UserId)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

            try
            {
                var result = this.cartBusiness.UpdateCartList(cartList, userId);
                if (result)
                {
                    nlog.LogInfo("Updated Cart Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Updated Successfully", Data = cartList });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Cart Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("DeleteCart")]
        public ActionResult DeleteCart(int CartId)
        {
            try
            {
                var result = this.cartBusiness.DeleteCart(CartId);
                if (result)
                {
                    nlog.LogInfo("Deleted Cart Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Deleted Successfully"});
                }
                return this.BadRequest(new { Status = false, Message = "Cart Deletion Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
