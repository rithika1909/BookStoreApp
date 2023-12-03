using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.Book
{
    public class Book
    {
       
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string BookDescription { get; set; }
        public string BookAuthor { get; set; }
        public string BookImage { get; set; }
        public int BookCount { get; set; }
        public int BookPrice { get; set; }
        public int Rating { get; set; }
    }
}
