using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class OrderPlaced
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "CustomerId is null")]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "CartId is null")]
        public int CartId { get; set; }
       
        public Cart Cart { get; set; }
        
    }
}
