using BookStoreCommon.Book;
using BookStoreRepository.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IBookRepo
    {
        public  Task<int> AddBook(Book obj);

        public IEnumerable<Book> GetAllBooks();

        public bool UpdateBook(Book obj);

        public bool DeleteBook(int BookId);

        public bool Image(IFormFile file, int BookId);


        public Book GetBookById(int BookId);



    }
}
