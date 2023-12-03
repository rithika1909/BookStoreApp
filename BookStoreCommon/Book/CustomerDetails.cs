using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.Book
{
    public class CustomerDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

       
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]

        [Required(ErrorMessage = "FullName is null")]
        public string FullName {  get; set; }

        [Required(ErrorMessage = "MobileNumber is null")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Address is null")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CityOrTown is null")]
        public string CityOrTown { get; set; }

        [Required(ErrorMessage = "State is null")]
        public string State {  get; set; }

        public Types Types { get; set; }


       
       

    }
}
