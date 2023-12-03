using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishListId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }
     
        //public  virtual UserRegister UserRegister { get; set; }
        public virtual Book Book { get; set; }

        public bool IsAvailable { get; set; }

       

    }
}
