using BookStoreBusiness.IBusiness;
using BookStoreCommon.Book;
using BookStoreRepository.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BookStoreBusiness.Business
{
    public class BookBusiness : IBookBusiness
    {
        public readonly IBookRepo bookRepo;

        NlogUtility nlog = new NlogUtility();
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        public Task<int> AddBook(Book obj)
        {
            try
            {
                var result = this.bookRepo.AddBook(obj);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                var result = this.bookRepo.GetAllBooks();
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public bool UpdateBook(Book obj)
        {
            try
            {
                var result = this.bookRepo.UpdateBook(obj);
                return result;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                var reult = this.bookRepo.DeleteBook(BookId);
                return reult;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public bool Image(IFormFile file, int BookId)
        {
            try
            {
                var reult = this.bookRepo.Image(file, BookId);
                return reult;

            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }


        }

        public Book GetBookById(int BookId)
        {
            try
            {
                var result = this.bookRepo.GetBookById(BookId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }

        }







    }
}
