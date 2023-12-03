using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using NLog.Fluent;
using Utility;

namespace BookStoreApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        public readonly IWishListBusiness wishlistBusiness;

        public int userid;
        public WishListController(IWishListBusiness wishlistBusiness)
        {
            this.wishlistBusiness = wishlistBusiness;
        }

        NlogUtility nlog = new NlogUtility();


        [HttpPost]
        [Route("AddWishlist")]
        public ActionResult AddBookToWishList(WishList obj)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.wishlistBusiness.AddBookToWishList(obj, userid);
                if (result != null)
                {
                    nlog.LogInfo("Wishlist Added Successfully");
                    return this.Ok(new { Status = true, Message = "Wishlist Added Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Adding wishlist Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllWishListBooks")]
        public ActionResult GetAllWishListBooks()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.wishlistBusiness.GetAllWishListBooks(userId);
                if (result != null)
                {
                    nlog.LogInfo("Retrieved Successfully");
                    return this.Ok(new { Status = true, Message = "All Wishlist Books Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Wishlist Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateWishList")]
        public ActionResult UpdateWishList(WishList wishlist)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

            try
            {
                var result = this.wishlistBusiness.UpdateWishList(wishlist, userId);
                if (result)
                {
                    nlog.LogInfo("Wishlist Updated Successfully");
                    return this.Ok(new { Status = true, Message = "Wishlist Updated Successfully", Data = wishlist });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Wishlist Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("DeleteWishList")]
        public ActionResult DeleteWishList( int WishListId)
        {
            try
            {
                var result = this.wishlistBusiness.DeleteWishList(WishListId);
                if (result)
                {
                    nlog.LogInfo("Wishlist Added Successfully");
                    return this.Ok(new { Status = true, Message = "Wishlist Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Wishlist Deletion Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
