using BookStoreCommon.Book;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface IBookBusiness
    {
        public Task<int> AddBook(Book obj);

        public IEnumerable<Book> GetAllBooks();

        public bool UpdateBook(Book obj);

        public bool DeleteBook(int BookId);

        public bool Image(IFormFile file, int BookId);


        public Book GetBookById(int BookId);




    }
}
