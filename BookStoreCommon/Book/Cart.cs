using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]


        [Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public int BookCount {  get; set; }

        public bool IsAvailable { get; set; }



    }
}
