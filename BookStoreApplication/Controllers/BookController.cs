using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Utility;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly IBookBusiness bookBusiness;

        NlogUtility nlog = new NlogUtility();
        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        [HttpPost]
        [Route("AddBook")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddBook(Book book)
        {
            try
            {
                var result = await this.bookBusiness.AddBook(book);
                if (result != 0)
                {
                    nlog.LogInfo("Book Added Successfully");
                    return this.Ok(new { Status = true, Message = "Book Added Successfully", Data = book });
                }
                return this.BadRequest(new { Status = false, Message = "Adding Book Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var result = this.bookBusiness.GetAllBooks();
                if (result != null)
                {
                    nlog.LogInfo("Retrieved Books Successfully");
                    return this.Ok(new { Status = true, Message = "All Books Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Books Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateBook(Book book)
        {
            try
            {
                
                var result = this.bookBusiness.UpdateBook(book);
                if (result)
                {
                    nlog.LogInfo("Updated Book Successfully");
                    return this.Ok(new { Status = true, Message = "Book Updated Successfully", Data = book });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Book Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("DeleteBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
               
                var result = this.bookBusiness.DeleteBook(BookId);
                if (result)
                {
                    nlog.LogInfo("Deleted Book Successfully");
                    return this.Ok(new { Status = true, Message = "Book Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Book Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public ActionResult Image(IFormFile file, int BookId)
        {
            try
            {
               
                var result = this.bookBusiness.Image(file, BookId);
                if (result)
                {
                    nlog.LogInfo("Image Uploaded Successfully");
                    return this.Ok(new { Status = true, Message = "Image Uploaded Successfully", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Uploading Image Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetBookById")]
        public async Task<ActionResult> GetBookById(int BookId)
        {
            try
            {
                var result = this.bookBusiness.GetBookById(BookId);
                if (result != null)
                {
                    nlog.LogInfo("Retrieved Book  Successfully");
                    return this.Ok(new { Status = true, Message = " Books Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Books Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
