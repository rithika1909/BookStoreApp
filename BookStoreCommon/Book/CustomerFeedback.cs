using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class CustomerFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public int BookId { get; set; }
        [ForeignKey("BookId")]

        [Required(ErrorMessage = "CustomerDescription is null")]
        public string CustomerDescription { get; set; }

        [Required(ErrorMessage = "Rating is null")]
        public int Ratings{ get; set; }


    }
}